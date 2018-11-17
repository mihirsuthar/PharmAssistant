using PharmAssistant.Models;
using PharmAssistant.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PharmAssistant.Controllers
{
    public class PurchaseOrdersController : Controller
    {
        //private List<PurchaseOrderItem> PurchaseItems = new List<PurchaseOrderItem>();
        //private PurchaseOrderViewModel OrderViewModel = new PurchaseOrderViewModel();

        static PharmAssistantContext db = new PharmAssistantContext();
        static List<PaymentMode> PaymentModeList = new List<PaymentMode> {
                                                    new PaymentMode{ ModeId = "Cash", ModeName = "Cash" },
                                                    new PaymentMode{ ModeId = "Credit", ModeName = "Credit" },
                                                    new PaymentMode{ ModeId = "NEFT", ModeName = "NEFT" }};


        static SelectList Medicines = new SelectList(db.Medicines.ToList(), "MedicineId", "Name"),
                            MedicineCategories = new SelectList(db.MedicineCategories.ToList(), "CategoryId", "Name"),
                            Suppliers = new SelectList(db.Suppliers.ToList(), "SupplierId", "Name"),
                            PaymentModes = new SelectList(PaymentModeList, "ModeId", "ModeName");

        // GET: PurchaseOrders
        public ActionResult PurchaseOrdersList()
        {
            try
            {
                using (PharmAssistantContext db = new PharmAssistantContext())
                {
                    return View(db.PurchaseOrders.ToList());
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return View(new List<PurchaseOrder>());
            }
        }

        public ActionResult NewPurchaseOrder()
        {
            string[] uri = Request.UrlReferrer == null ? new string[] { "PurchaseOrdersList" } : Request.UrlReferrer.ToString().Split('/');

            PurchaseOrderViewModel PurchaseOrderModel = new PurchaseOrderViewModel();

            if (uri.Contains("PurchaseOrdersList"))
            {
                //ViewBag.PurchaseOrderId = DateTime.Now.ToString("ffffssmmhhMMddyyyy");
                //PurchaseOrderModel.PurchaseOrder = new PurchaseOrder { PurchaseOrderId = Convert.ToInt64(DateTime.Now.ToString("ffffssmmhhMMddyyyy")) };
                Session["PurchaseOrderId"] = DateTime.Now.ToString("ffffssmmhhMMddyyyy");
                Session.Remove("PurchaseOrderModel");
            }
            else
            {
                PurchaseOrderModel = (PurchaseOrderViewModel)Session["PurchaseOrderModel"];
                //PurchaseOrderModel.PurchaseOrder = new PurchaseOrder { PurchaseOrderId = Convert.ToInt64(DateTime.Now.ToString("ffffssmmhhMMddyyyy")) };
            }

            PurchaseOrderModel.PurchaseOrder = new PurchaseOrder { PurchaseDate = DateTime.Now };

            FillDropdowns();
            return View(PurchaseOrderModel);
        }

        public ActionResult AddMedicineToOrder(PurchaseOrderItem PurchaseOrderItem)
        {
            using (PharmAssistantContext db = new PharmAssistantContext())
            {
                var medicine = db.Medicines.Where(m => m.MedicineId == PurchaseOrderItem.MedicineId).FirstOrDefault();
                PurchaseOrderItem.SellingPrice = medicine.SellingPrice;
                PurchaseOrderItem.MedicineName = medicine.MedicineName;

                PurchaseOrderViewModel PurchaseOrderModel;

                if (Session["PurchaseOrderModel"] == null)
                {
                    PurchaseOrderModel = new PurchaseOrderViewModel();
                    PurchaseOrderModel.PurchaseOrderItems = new List<PurchaseOrderItem>();
                    PurchaseOrderModel.PurchaseOrderItems.Add(PurchaseOrderItem);
                    Session["PurchaseOrderModel"] = PurchaseOrderModel;
                }
                else
                {
                    PurchaseOrderModel = (PurchaseOrderViewModel)Session["PurchaseOrderModel"];
                    PurchaseOrderModel.PurchaseOrderItems.Add(PurchaseOrderItem);
                    Session["PurchaseOrderModel"] = PurchaseOrderModel;
                }   

                return PartialView("_PurchaseOrderItems", PurchaseOrderModel);
            }
        }

        public ActionResult SavePurchaseOrder(PurchaseOrderViewModel PurchaseOrderModel)
        {
            double OrderCost = 0;

            ICollection<PurchaseOrderItem> OrderItems = ((PurchaseOrderViewModel)Session["PurchaseOrderModel"]).PurchaseOrderItems;
            foreach (var item in OrderItems)
            {
                item.PurchaseOrderId = Session["PurchaseOrderId"].ToString();
                OrderCost += item.Quantity * item.SellingPrice;
            }

            PurchaseOrderModel.PurchaseOrderItems = OrderItems;
            PurchaseOrderModel.PurchaseOrder.OrderCost = Math.Round(OrderCost, 2);

            using (PharmAssistantContext db = new PharmAssistantContext())
            {
                try
                {
                    db.Database.BeginTransaction();

                    db.PurchaseOrders.Add(PurchaseOrderModel.PurchaseOrder);
                    db.SaveChanges();

                    db.PurchaseOrderItems.AddRange(PurchaseOrderModel.PurchaseOrderItems);
                    db.SaveChanges();

                    db.Database.CurrentTransaction.Commit();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    db.Database.CurrentTransaction.Rollback();
                    FillDropdowns();
                    return View("NewPurchaseOrder", PurchaseOrderModel);
                    throw;
                }
            }


            return RedirectToAction("PurchaseOrdersList");
        }

        public ActionResult CancelPurchaseOrder()
        {
            Session.Remove("PurchaseOrderModel");
            Session.Remove("PurchaseOrderId");
            return RedirectToAction("PurchaseOrdersList");
        }

        [HttpPost]
        public JsonResult GetMedicines(int CategoryId)
        {
            try
            {
                using (PharmAssistantContext db = new PharmAssistantContext())
                {
                    var Medicines = db.Medicines.Where(m => m.CategoryId == CategoryId).Select(m => new { MedicineId = m.MedicineId, Name = m.MedicineName.Trim() }).ToList();
                    return Json(Medicines, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return Json(new { }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult DeletePurchaseOrderItem(int id)
        {
            PurchaseOrderViewModel PurchaseOrderModel = (PurchaseOrderViewModel)Session["PurchaseOrderModel"];            
            PurchaseOrderModel.PurchaseOrderItems.Remove(PurchaseOrderModel.PurchaseOrderItems.Where(i => i.MedicineId == id).FirstOrDefault());

            FillDropdowns();
            //return View("NewPurchaseOrder", PurchaseOrderModel);
            return RedirectToAction("NewPurchaseOrder");

        }

        private void FillDropdowns()
        {
            ViewBag.Medicines = Medicines;
            ViewBag.MedicineCategories = MedicineCategories;
            ViewBag.Suppliers = Suppliers;
            ViewBag.PaymentModes = PaymentModes;
        }
    }

    public class PaymentMode
    {
        public string ModeId { get; set; }
        public string ModeName { get; set; }
    }
}