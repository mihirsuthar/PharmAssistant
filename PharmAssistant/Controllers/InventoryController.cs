using PharmAssistant.Models;
using PharmAssistant.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
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

        [HttpPost]
        public JsonResult GetStockByCategory(int CategoryId)
        {
            return Json(GetInventoryStock(CategoryId), JsonRequestBehavior.AllowGet);
        }

        // GET: Inventory
        public ActionResult AvailableStock()
        {
            //if(TempData["UpdateSuccess"] != null)
            //{
            //    ViewBag.UpdateSuccess = TempData["UpdateSuccess"];
            //}
            return View(GetInventoryStock());
        }

        public ActionResult InventorySettings()
        {
            return View(GetInventorySettings());
        }

        [HttpPost]
        public ActionResult InventorySettingsByCategory(int CategoryId)
        {
            return Json(GetInventorySettings(CategoryId), JsonRequestBehavior.AllowGet);
        }

        public ActionResult UpdateInventorySetting(Medicine medicine)
        {
            try
            {
                if (medicine.StockCapacity == 0 || medicine.ReorderLevel == 0 || medicine.BufferLevel == 0)
                {
                    return Json("Invalid Data", JsonRequestBehavior.AllowGet);
                }

                using (PharmAssistantContext db = new PharmAssistantContext())
                {
                    db.Medicines.Attach(medicine);
                    //db.Entry(medicine).State = EntityState.Modified;
                    db.Entry(medicine).Property("StockCapacity").IsModified = true;
                    db.Entry(medicine).Property("ReorderLevel").IsModified = true;
                    db.Entry(medicine).Property("BufferLevel").IsModified = true;
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            //TempData["UpdateSuccess"] = "Record updated successfully.";
            //return RedirectToAction("AvailableStock");
            return Json(GetInventorySettings(), JsonRequestBehavior.AllowGet);
        }

        public List<InventoryViewModel> GetInventorySettings(int CategoryId = -1)
        {
            List<InventoryViewModel> InventoryStock;
            try
            {
                ViewBag.MedicineCategories = MedicineCategories;
                using (PharmAssistantContext db = new PharmAssistantContext())
                {
                    InventoryStock = new List<InventoryViewModel>();
                    var InventoryData = from m in db.Medicines
                                        select new
                                        {
                                            CategoryId = m.CategoryId,
                                            MedicineId = m.MedicineId,
                                            MedicineName = m.MedicineName,
                                            StockCapacity = m.StockCapacity,
                                            ReorderLevel = m.ReorderLevel,
                                            BufferLevel = m.BufferLevel
                                        };

                    if (CategoryId != -1)
                    {
                        InventoryData = InventoryData.Where(m => m.CategoryId == CategoryId);
                    }

                    foreach (var data in InventoryData)
                    {
                        InventoryStock.Add(new InventoryViewModel
                        {
                            MedicineId = data.MedicineId,
                            MedicineName = data.MedicineName,
                            StockCapacity = data.StockCapacity,
                            ReorderLevel = data.ReorderLevel,
                            BufferLevel = data.BufferLevel
                        });
                    }

                    return InventoryStock;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        List<InventoryViewModel> GetInventoryStock(int CategoryId = -1)
        {
            try
            {
                using (PharmAssistantContext db = new PharmAssistantContext())
                {
                    ViewBag.MedicineCategories = MedicineCategories;

                    //var InventoryData = null;

                    InventoryStock = new List<InventoryViewModel>();

                    var InventoryData = from m in db.Medicines
                                        join se in db.StockEntries on m.MedicineId equals se.MedicineId
                                        join po in db.PurchaseOrders on se.PurchaseOrderId equals po.PurchaseOrderId
                                        orderby m.MedicineName, se.ExpiryDate
                                        select new
                                        {
                                            CategoryId = m.CategoryId,
                                            MedicineId = m.MedicineId,
                                            MedicineName = m.MedicineName,
                                            ExpiryDate = se.ExpiryDate,
                                            StockCapacity = m.StockCapacity,
                                            ReorderLevel = m.ReorderLevel,
                                            BufferLevel = m.BufferLevel,
                                            BatchNumber = se.BatchNumber,
                                            Manufacturer = m.Manufacturer.ManufacturerName,
                                            Supplier = po.Supplier.SupplierName,
                                            StockUnits = se.Quantity
                                        };

                    if (CategoryId != -1)
                    {
                        InventoryData = InventoryData.Where(i => i.CategoryId == CategoryId);
                    }

                    foreach (var data in InventoryData)
                    {
                        InventoryStock.Add(new InventoryViewModel
                        {
                            MedicineId = data.MedicineId,
                            MedicineName = data.MedicineName,
                            ExpiryDate = (DateTime)data.ExpiryDate,
                            StockCapacity = data.StockCapacity,
                            ReorderLevel = data.ReorderLevel,
                            BufferLevel = data.BufferLevel,
                            BatchNumber = data.BatchNumber,
                            ManufacturerName = data.Manufacturer,
                            Supplier = data.Supplier,
                            StockUnits = data.StockUnits
                        });
                    }
                    //if (CategoryId == -1)
                    //{
                    //    var InventoryData = from m in db.Medicines
                    //                        join se in db.StockEntries on m.MedicineId equals se.MedicineId
                    //                        join po in db.PurchaseOrders on se.PurchaseOrderId equals po.PurchaseOrderId
                    //                        orderby m.MedicineName, se.ExpiryDate
                    //                        select new
                    //                        {
                    //                            CategoryId = m.CategoryId,
                    //                            MedicineId = m.MedicineId,
                    //                            MedicineName = m.MedicineName,
                    //                            ExpiryDate = se.ExpiryDate,
                    //                            StockCapacity = m.StockCapacity,
                    //                            ReorderLevel = m.ReorderLevel,
                    //                            BufferLevel = m.BufferLevel,
                    //                            BatchNumber = se.BatchNumber,
                    //                            Manufacturer = m.Manufacturer.ManufacturerName,
                    //                            Supplier = po.Supplier.SupplierName,
                    //                            StockUnits = se.Quantity
                    //                        };

                    //    InventoryData.Where(i => i.CategoryId == CategoryId);

                    //    foreach (var data in InventoryData)
                    //    {
                    //        InventoryStock.Add(new InventoryViewModel
                    //        {
                    //            MedicineId = data.MedicineId,
                    //            MedicineName = data.MedicineName,
                    //            ExpiryDate = (DateTime)data.ExpiryDate,
                    //            StockCapacity = data.StockCapacity,
                    //            ReorderLevel = data.ReorderLevel,
                    //            BufferLevel = data.BufferLevel,
                    //            BatchNumber = data.BatchNumber,
                    //            ManufacturerName = data.Manufacturer,
                    //            Supplier = data.Supplier,
                    //            StockUnits = data.StockUnits
                    //        });
                    //    }
                    //}
                    //else
                    //{
                    //    var InventoryData = from m in db.Medicines
                    //                        join se in db.StockEntries on m.MedicineId equals se.MedicineId
                    //                        join po in db.PurchaseOrders on se.PurchaseOrderId equals po.PurchaseOrderId
                    //                        where m.CategoryId == CategoryId
                    //                        orderby m.MedicineName, se.ExpiryDate
                    //                        select new
                    //                        {
                    //                            MedicineId = m.MedicineId,
                    //                            MedicineName = m.MedicineName,
                    //                            ExpiryDate = se.ExpiryDate,
                    //                            StockCapacity = m.StockCapacity,
                    //                            ReorderLevel = m.ReorderLevel,
                    //                            BufferLevel = m.BufferLevel,
                    //                            BatchNumber = se.BatchNumber,
                    //                            Manufacturer = m.Manufacturer.ManufacturerName,
                    //                            Supplier = po.Supplier.SupplierName,
                    //                            StockUnits = se.Quantity
                    //                        };

                    //    foreach (var data in InventoryData)
                    //    {
                    //        InventoryStock.Add(new InventoryViewModel
                    //        {
                    //            MedicineId = data.MedicineId,
                    //            MedicineName = data.MedicineName,
                    //            ExpiryDate = (DateTime)data.ExpiryDate,
                    //            StockCapacity = data.StockCapacity,
                    //            ReorderLevel = data.ReorderLevel,
                    //            BufferLevel = data.BufferLevel,
                    //            BatchNumber = data.BatchNumber,
                    //            ManufacturerName = data.Manufacturer,
                    //            Supplier = data.Supplier,
                    //            StockUnits = data.StockUnits
                    //        });
                    //    }
                    //}
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return InventoryStock;
        }


    }
}