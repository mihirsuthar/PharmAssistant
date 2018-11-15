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

        static SelectList Medicines = new SelectList(db.Medicines.ToList(), "MedicineId", "Name"),
                            MedicineCategories = new SelectList(db.MedicineCategories.ToList(), "CategoryId", "Name"),
                            Suppliers = new SelectList(db.Suppliers.ToList(), "SupplierId", "Name");

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

            string[] uri = Request.UrlReferrer == null ? new string[1] : Request.UrlReferrer.ToString().Split('/');

            PurchaseOrderViewModel PurchaseOrderModel = new PurchaseOrderViewModel();

            if (uri.Contains("PurchaseOrdersList"))
            {
                ViewBag.PurchaseOrderId = DateTime.Now.ToString("ffffssmmhhMMddyyyy");
                Session.Remove("PurchaseOrderModel");
            }
            else
            {
                PurchaseOrderModel = (PurchaseOrderViewModel)Session["PurchaseOrderModel"];
            }

            FillDropdowns();
            return View(PurchaseOrderModel);
        }

        public ActionResult AddMedicineToOrder(PurchaseOrderItem PurchaseOrderItem)
        {
            using (PharmAssistantContext db = new PharmAssistantContext())
            {
                var medicine = db.Medicines.Where(m => m.MedicineId == PurchaseOrderItem.MedicineId).FirstOrDefault();
                PurchaseOrderItem.SellingPrice = medicine.SellingPrice;
                PurchaseOrderItem.MedicineName = medicine.Name;

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

        [HttpPost]
        public JsonResult GetMedicines(int CategoryId)
        {
            try
            {
                using (PharmAssistantContext db = new PharmAssistantContext())
                {
                    var Medicines = db.Medicines.Where(m => m.CategoryId == CategoryId).Select(m => new { MedicineId = m.MedicineId, Name = m.Name.Trim() }).ToList();
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
        }
    }
}