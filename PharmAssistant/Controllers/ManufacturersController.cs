using PharmAssistant.Models;
using PharmAssistant.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;

namespace PharmAssistant.Controllers
{
    public class ManufacturersController : Controller
    {
        [OutputCache(Duration = 20, Location = OutputCacheLocation.Server)]
        public ActionResult Index()
        {
            ManufacturerViewModel manufacturerData = new ManufacturerViewModel();
            manufacturerData.Manufacturer = new Manufacturer();
            manufacturerData.ManufacturersList = GetAllManufacturers();

            return View(manufacturerData);
            //return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddManufacturer(Manufacturer manufacturer)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    using (PharmAssistantContext db = new PharmAssistantContext())
                    {
                        db.Manufacturers.Add(manufacturer);
                        db.SaveChanges();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

                return PartialView("_ManufacturersList", GetAllManufacturers());
                //return View("Index", new ManufacturerViewModel { Manufacturer = new Manufacturer(), ManufacturersList = GetAllManufacturers()});
                //return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("Name", "Invalid Name");
                //return PartialView("_ManufacturersList", GetAllManufacturers());                
                //return View("Index", new ManufacturerViewModel { Manufacturer = new Manufacturer(), ManufacturersList = GetAllManufacturers() });
                return RedirectToAction("Index");
            }
        }

        public ActionResult DeleteManufacturer(int Id)
        {
            using (PharmAssistantContext db = new PharmAssistantContext())
            {
                try
                {
                    Manufacturer manufacturer = db.Manufacturers.Where(m => m.ManufacturerId == Id).FirstOrDefault();
                    if (manufacturer != null)
                    {
                        db.Manufacturers.Remove(manufacturer);
                        db.SaveChanges();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Excption while deletion of Manufacturer: " + ex.Message);
                }
            }

            return RedirectToAction("Index");
        }

        public ActionResult EditManufacturer(int Id)
        {
            try
            {
                ManufacturerViewModel manufacturerData = new ManufacturerViewModel();

                using (PharmAssistantContext db = new PharmAssistantContext())
                {
                    manufacturerData.Manufacturer = db.Manufacturers.Where(s => s.ManufacturerId == Id).FirstOrDefault();
                    manufacturerData.ManufacturersList = GetAllManufacturers();
                }

                return View("Index", manufacturerData);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception while editing manufacturer: " + ex.Message);
                return RedirectToAction("Index");
            }
        }

        public ActionResult UpdateManufacturer(Manufacturer manufacturer)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    using (PharmAssistantContext db = new PharmAssistantContext())
                    {
                        db.Manufacturers.Attach(manufacturer);
                        db.Entry(manufacturer).State = EntityState.Modified;

                        db.SaveChanges();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

                return PartialView("_ManufacturersList", GetAllManufacturers());
                //return View("Index", new ManufacturerViewModel { Manufacturer = new Manufacturer(), ManufacturersList = GetAllManufacturers()});
                //return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("Name", "Invalid Name");
                //return PartialView("_ManufacturersList", GetAllManufacturers());                
                //return View("Index", new ManufacturerViewModel { Manufacturer = new Manufacturer(), ManufacturersList = GetAllManufacturers() });
                return RedirectToAction("Index");
            }
        }

        private List<Manufacturer> GetAllManufacturers()
        {
            try
            {
                using (PharmAssistantContext db = new PharmAssistantContext())
                {
                    return db.Manufacturers.ToList();
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