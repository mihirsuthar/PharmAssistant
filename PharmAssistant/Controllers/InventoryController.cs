using PharmAssistant.Models;
using PharmAssistant.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PharmAssistant.Controllers
{
    public class InventoryController : Controller
    {
        static PharmAssistantContext db = new PharmAssistantContext();
        static SelectList MedicineCategories = new SelectList(db.MedicineCategories.ToList(), "CategoryId", "MedicineCategoryName");

        // GET: Inventory
        public ActionResult AvailableStock()
        {
            using (PharmAssistantContext db = new PharmAssistantContext())
            {
                ViewBag.MedicineCategories = MedicineCategories;

                //var InventoryData = db.Medicines.Join(
                //                            db.StockEntries, 
                //                            se => se.MedicineId, 
                //                            m => m.MedicineId,
                //                            (medicine, stockentry) => new
                //                            {
                //                                MedicineId = medicine.MedicineId,
                //                                MedicineName = medicine.MedicineName,
                //                                ExpiryDate = stockentry.ExpiryDate,
                //                                Manufacturer = medicine.Manufacturer.ManufacturerName,
                //                                Suppliers = medicine.Suppliers.ToList(),
                //                                StockUnits = stockentry.Quantity
                //                            }).ToList();

                var InventoryData = from m in db.Medicines
                                    join se in db.StockEntries on m.MedicineId equals se.MedicineId
                                    join po in db.PurchaseOrders on se.PurchaseOrderId equals po.PurchaseOrderId
                                    select new {
                                        MedicineId = m.MedicineId,
                                        MedicineName = m.MedicineName,
                                        ExpiryDate = se.ExpiryDate,
                                        ReorderLevel = m.ReorderLevel,
                                        BufferLevel = m.BufferLevel,
                                        Manufacturer = m.Manufacturer.ManufacturerName,
                                        Supplier = po.Supplier.SupplierName,
                                        StockUnits = se.Quantity

                                    };

                List < InventoryViewModel > InventoryStock = new List<InventoryViewModel>();

                foreach(var data in InventoryData)
                {
                    InventoryStock.Add(new InventoryViewModel
                    {
                        MedicineId = data.MedicineId,
                        MedicineName = data.MedicineName,
                        ExpiryDate = (DateTime)data.ExpiryDate,
                        ReorderLevel = data.ReorderLevel,
                        BufferLevel = data.BufferLevel,
                        ManufacturerName = data.Manufacturer,
                        Supplier = data.Supplier,
                        StockUnits = data.StockUnits
                    });
                }


                return View(InventoryStock);
            }


        }
    }
}