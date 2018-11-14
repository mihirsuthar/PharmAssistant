using PharmAssistant.Models;
using PharmAssistant.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using System.Web.UI;

namespace PharmAssistant.Controllers
{
    public class SuppliersController : Controller
    {
        // GET: Supplier
        [OutputCache(Duration = 20, Location = OutputCacheLocation.Server)]
        public ActionResult Index()
        {
            SupplierViewModel supplierdata = new SupplierViewModel();
            supplierdata.Supplier = new Supplier();
            supplierdata.SuppliersList = GetAllSuppliers();

            return View(supplierdata);
            //return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddSupplier(Supplier supplier)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    using (PharmAssistantContext db = new PharmAssistantContext())
                    {
                        db.Suppliers.Add(supplier);
                        db.SaveChanges();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

                return PartialView("_SuppliersList", GetAllSuppliers());
                //return View("Index", new SupplierViewModel { Supplier = new Supplier(), SuppliersList = GetAllSuppliers()});
                //return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("Name", "Invalid Name");
                //return PartialView("_SuppliersList", GetAllSuppliers());                
                //return View("Index", new SupplierViewModel { Supplier = new Supplier(), SuppliersList = GetAllSuppliers() });
                return RedirectToAction("Index");
            }
        }

        public ActionResult DeleteSupplier(int Id)
        {
            using (PharmAssistantContext db = new PharmAssistantContext())
            {
                try
                {
                    Supplier supplier = db.Suppliers.Where(s => s.SupplierId == Id).FirstOrDefault();
                    if (supplier != null)
                    {
                        db.Suppliers.Remove(supplier);
                        db.SaveChanges();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Excption while deletion of Supplier: " + ex.Message);
                }
            }

            return RedirectToAction("Index");
        }

        public ActionResult EditSupplier(int Id)
        {
            try
            {
                SupplierViewModel supplierData = new SupplierViewModel();

                using (PharmAssistantContext db = new PharmAssistantContext())
                {
                    supplierData.Supplier = db.Suppliers.Where(s => s.SupplierId == Id).FirstOrDefault();
                    supplierData.SuppliersList = GetAllSuppliers();
                }

                return View("Index", supplierData);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception while editing supplier: " + ex.Message);
                return RedirectToAction("Index");
            }
        }

        public ActionResult UpdateSupplier(Supplier supplier)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    using (PharmAssistantContext db = new PharmAssistantContext())
                    {
                        db.Suppliers.Attach(supplier);
                        db.Entry(supplier).State = EntityState.Modified;

                        db.SaveChanges();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

                return PartialView("_SuppliersList", GetAllSuppliers());
                //return View("Index", new SupplierViewModel { Supplier = new Supplier(), SuppliersList = GetAllSuppliers()});
                //return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("Name", "Invalid Name");
                //return PartialView("_SuppliersList", GetAllSuppliers());                
                //return View("Index", new SupplierViewModel { Supplier = new Supplier(), SuppliersList = GetAllSuppliers() });
                return RedirectToAction("Index");
            }
        }

        private List<Supplier> GetAllSuppliers()
        {
            try
            {
                using (PharmAssistantContext db = new PharmAssistantContext())
                {
                    return db.Suppliers.ToList();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return null;
        }        
    }
}