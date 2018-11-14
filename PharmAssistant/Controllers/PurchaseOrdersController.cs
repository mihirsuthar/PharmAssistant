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
            FillDropdowns();
            return View(new PurchaseOrderViewModel());
        }

        private void FillDropdowns()
        {
            ViewBag.Medicines = Medicines;
            ViewBag.MedicineCategories = MedicineCategories;
            ViewBag.Suppliers = Suppliers;            
        }
    }
}