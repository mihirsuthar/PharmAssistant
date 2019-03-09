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
        SelectList MedicineCategories;// = new SelectList(db.MedicineCategories.ToList(), "CategoryId", "MedicineCategoryName");
        List<InventoryViewModel> InventoryStock;

        public InventoryController()
        {
            MedicineCategories = new SelectList(db.MedicineCategories.ToList(), "CategoryId", "MedicineCategoryName");
        }

        public JsonResult GetStockByCategory(int CategoryId)
        {
            try
            {
                using (PharmAssistantContext db = new PharmAssistantContext())
                {
                    ViewBag.MedicineCategories = MedicineCategories;

                    var InventoryData = from m in db.Medicines
                                        join se in db.StockEntries on m.MedicineId equals se.MedicineId
                                        join po in db.PurchaseOrders on se.PurchaseOrderId equals po.PurchaseOrderId
                                        where m.MedicineCategory.CategoryId == CategoryId
                                        orderby m.MedicineName, se.ExpiryDate
                                        select new
                                        {
                                            MedicineId = m.MedicineId,
                                            MedicineName = m.MedicineName,
                                            ExpiryDate = se.ExpiryDate,
                                            ReorderLevel = m.ReorderLevel,
                                            BufferLevel = m.BufferLevel,
                                            BatchNumber = se.BatchNumber,
                                            Manufacturer = m.Manufacturer.ManufacturerName,
                                            Supplier = po.Supplier.SupplierName,
                                            StockUnits = se.Quantity
                                        };

                    InventoryStock = new List<InventoryViewModel>();

                    foreach (var data in InventoryData)
                    {
                        InventoryStock.Add(new InventoryViewModel
                        {
                            MedicineId = data.MedicineId,
                            MedicineName = data.MedicineName,
                            ExpiryDate = (DateTime)data.ExpiryDate,
                            ReorderLevel = data.ReorderLevel,
                            BufferLevel = data.BufferLevel,
                            BatchNumber = data.BatchNumber,
                            ManufacturerName = data.Manufacturer,
                            Supplier = data.Supplier,
                            StockUnits = data.StockUnits
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            
            return Json(InventoryStock, JsonRequestBehavior.AllowGet);
        }

        // GET: Inventory
        public ActionResult AvailableStock()
        {
            try
            {
                using (PharmAssistantContext db = new PharmAssistantContext())
                {
                    ViewBag.MedicineCategories = MedicineCategories;

                    var InventoryData = from m in db.Medicines
                                        join se in db.StockEntries on m.MedicineId equals se.MedicineId
                                        join po in db.PurchaseOrders on se.PurchaseOrderId equals po.PurchaseOrderId
                                        orderby m.MedicineName, se.ExpiryDate
                                        select new
                                        {
                                            MedicineId = m.MedicineId,
                                            MedicineName = m.MedicineName,
                                            ExpiryDate = se.ExpiryDate,
                                            ReorderLevel = m.ReorderLevel,
                                            BufferLevel = m.BufferLevel,
                                            BatchNumber = se.BatchNumber,
                                            Manufacturer = m.Manufacturer.ManufacturerName,
                                            Supplier = po.Supplier.SupplierName,
                                            StockUnits = se.Quantity
                                        };

                    InventoryStock = new List<InventoryViewModel>();

                    foreach (var data in InventoryData)
                    {
                        InventoryStock.Add(new InventoryViewModel
                        {
                            MedicineId = data.MedicineId,
                            MedicineName = data.MedicineName,
                            ExpiryDate = (DateTime)data.ExpiryDate,
                            ReorderLevel = data.ReorderLevel,
                            BufferLevel = data.BufferLevel,
                            BatchNumber = data.BatchNumber,
                            ManufacturerName = data.Manufacturer,
                            Supplier = data.Supplier,
                            StockUnits = data.StockUnits
                        });
                    }                    
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return View(InventoryStock);
        }
    }
}