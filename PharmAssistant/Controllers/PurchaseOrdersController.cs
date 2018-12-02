using PharmAssistant.Models;
using PharmAssistant.Models.ViewModels;
using Rotativa.MVC;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
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


        static SelectList Medicines = new SelectList(db.Medicines.ToList(), "MedicineId", "MedicineName"),
                            MedicineCategories = new SelectList(db.MedicineCategories.ToList(), "CategoryId", "MedicineCategoryName"),
                            Suppliers = new SelectList(db.Suppliers.ToList(), "SupplierId", "SupplierName"),
                            PaymentModes = new SelectList(PaymentModeList, "ModeId", "ModeName");

        // GET: PurchaseOrders
        public ActionResult PurchaseOrdersList()
        {
            try
            {
                using (PharmAssistantContext db = new PharmAssistantContext())
                {
                    //ICollection<PurchaseOrderViewModel> PurchaseOrdersList = new List<PurchaseOrderViewModel>();
                    ICollection<PurchaseOrder> PurchaseOrders = db.PurchaseOrders.Include("Supplier").ToList();
                    //ICollection<PurchaseOrderItem> PurchaseOrderItems = db.PurchaseOrderItems.ToList();
                    //PurchaseOrderViewModel order;

                    //foreach (var purchaseOrder in PurchaseOrders)
                    //{
                    //    order = new PurchaseOrderViewModel();

                    //    order.PurchaseOrder = purchaseOrder;
                    //    order.PurchaseOrderItems = PurchaseOrderItems.Where(i => i.PurchaseOrderId == purchaseOrder.PurchaseOrderId).ToList();

                    //    PurchaseOrdersList.Add(order);
                    //}

                    return View(PurchaseOrders);
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
                PurchaseOrderViewModel PurchaseOrderModel;

                try
                {
                    var medicine = db.Medicines.Where(m => m.MedicineId == PurchaseOrderItem.MedicineId).FirstOrDefault();
                    PurchaseOrderItem.SellingPrice = medicine.SellingPrice;
                    PurchaseOrderItem.MedicineName = medicine.MedicineName;

                    

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
                        if (PurchaseOrderModel.PurchaseOrderItems.Where(i => i.MedicineId == PurchaseOrderItem.MedicineId).Count() > 0)
                        {
                            throw new Exception(PurchaseOrderItem.MedicineName + " already exists in order.");
                        }

                        PurchaseOrderModel.PurchaseOrderItems.Add(PurchaseOrderItem);
                        Session["PurchaseOrderModel"] = PurchaseOrderModel;
                    }
                }
                catch (Exception ex)
                {
                    if(ex.Message.Contains(" already exists in order."))
                    {
                        Response.StatusCode = (int)HttpStatusCode.BadRequest;  // Or another code
                        return Json(new { message = ex.Message });
                    }
                    else
                    {
                        //return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, "Something went wrong. Try again later.");
                        Response.StatusCode = (int)HttpStatusCode.BadRequest;  // Or another code
                        return Json(new { message = "Something went wrong. Please try again." });
                    }
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

        public ActionResult DeleteOrder(string OrderId)
        {
            using (PharmAssistantContext db = new PharmAssistantContext())
            {
                try
                {
                    db.Database.BeginTransaction();
                    db.PurchaseOrderItems.RemoveRange(db.PurchaseOrderItems.Where(i => i.PurchaseOrderId == OrderId));
                    db.PurchaseOrders.RemoveRange(db.PurchaseOrders.Where(o => o.PurchaseOrderId == OrderId));
                    db.SaveChanges();
                    db.Database.CurrentTransaction.Commit();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    db.Database.CurrentTransaction.Rollback();
                    throw;
                }
            }

            return RedirectToAction("PurchaseOrdersList");
        }

        public ActionResult ReceiveOrder(string OrderId)
        {
            try
            {
                using (PharmAssistantContext db = new PharmAssistantContext())
                {
                    PurchaseOrderViewModel PurchaseOrderModel = new PurchaseOrderViewModel();
                    PurchaseOrderModel.PurchaseOrder = db.PurchaseOrders.Include("Supplier").Where(po => po.PurchaseOrderId == OrderId).FirstOrDefault();
                    PurchaseOrderModel.PurchaseOrderItems = db.PurchaseOrderItems.Where(i => i.PurchaseOrderId == OrderId).ToList();

                    return View(PurchaseOrderModel);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return View();
            }
        }

        [HttpPost]
        public ActionResult ReceiveOrder(string OrderId, ICollection<long> BatchNumbers, ICollection<DateTime> ExpiryDates)
        {
            using (PharmAssistantContext db = new PharmAssistantContext())
            {
                try
                {
                    db.Database.BeginTransaction();

                    PurchaseOrder order = db.PurchaseOrders.Where(o => o.PurchaseOrderId == OrderId).FirstOrDefault();
                    ICollection<PurchaseOrderItem> OrderItems = db.PurchaseOrderItems.Where(i => i.PurchaseOrderId == OrderId).ToList();

                    order.OrderStatus = true;

                    long[] batchNumbers = BatchNumbers.ToArray();
                    DateTime[] expiryDates = ExpiryDates.ToArray();
                    int counter = 0;

                    foreach (var OrderItem in OrderItems)
                    {
                        OrderItem.BatchNumber = batchNumbers[counter];
                        OrderItem.ExpiryDate = expiryDates[counter++];

                        db.PurchaseOrderItems.Attach(OrderItem);
                        db.Entry(OrderItem).State = EntityState.Modified;
                    }

                    db.SaveChanges();

                    db.PurchaseOrders.Attach(order);
                    db.Entry(order).State = EntityState.Modified;
                    db.SaveChanges();

                    db.Database.CurrentTransaction.Commit();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    db.Database.CurrentTransaction.Rollback();
                }
            }
            return RedirectToAction("PurchaseOrdersList");
        }

        public ActionResult GenerateInvoice(string OrderId)
        {
            //string FilePath = Server.MapPath("~/Invoices/") + OrderId + DateTime.Now.ToString("ddMMyyyyhhmmss") + ".pdf";
            //IronPdf.Installation.TempFolderPath = Server.MapPath("~/Invoices/");

            //IronPdf.Installation.TempFolderPath = @"F:\Projects\PharmAssistant\Invoices\";
            //string FilePath = @"F:\Projects\PharmAssistant\Invoices\" + OrderId + DateTime.Now.ToString("ddMMyyyyhhmmss") + ".pdf";

            //try
            //{   
            //    //var Renderer = new IronPdf.HtmlToPdf();

            //    //UriBuilder builder = new UriBuilder();
            //    //builder.Path = Url.Action("GeneratePdfInvoice", new { OrderId = OrderId });
            //    //IronPdf.PdfDocument document = Renderer.RenderUrlAsPdf(builder.Uri);//.SaveAs(FilePath);
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine(ex.Message);
            //}


            return new ActionAsPdf("GeneratePdfInvoice", new { OrderId = OrderId });
            //return File(FilePath, "application/pdf", OrderId + DateTime.Now.ToString("ddMMyyyyhhmmss") + ".pdf");
        }

        public ActionResult GeneratePdfInvoice(string OrderId)
        {
            PurchaseOrderViewModel PurchaseOrderData = new PurchaseOrderViewModel();
            using (PharmAssistantContext db = new PharmAssistantContext())
            {
                try
                {   
                    PurchaseOrderData.PurchaseOrder = db.PurchaseOrders.Include("Supplier").Where(o => o.PurchaseOrderId == OrderId).FirstOrDefault();
                    PurchaseOrderData.PurchaseOrderItems = db.PurchaseOrderItems.Where(i => i.PurchaseOrderId == OrderId).ToList();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            return View(PurchaseOrderData);
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
                    //return Json(new { }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return Json(new { }, JsonRequestBehavior.AllowGet);
            }
        }
        
        [HttpPost]
        public JsonResult GetMedicineCategories(int SupplierId)
        {
            try
            {
                using (PharmAssistantContext db = new PharmAssistantContext())
                {
                    var MedicineCategories = db.MedicineCategories.Where(m => m.Suppliers.Contains(db.Suppliers.Where(s => s.SupplierId == SupplierId).FirstOrDefault())).Select(m => new { CategoryId = m.CategoryId, MedicineCategotyName = m.MedicineCategoryName.Trim() }).ToList();
                    return Json(MedicineCategories, JsonRequestBehavior.AllowGet);
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