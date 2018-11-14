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
    public class MedicinesController : Controller
    {
        static PharmAssistantContext db = new PharmAssistantContext();

        static SelectList Manufacturers = new SelectList(db.Manufacturers.ToList(), "ManufacturerId", "Name"),
                            Categories = new SelectList(db.MedicineCategories.ToList(), "CategoryId", "Name"),
                            Suppliers = new SelectList(db.Suppliers.ToList(), "SupplierId", "Name"),
                            Shelves = new SelectList(db.Shelves.ToList(), "ShelfId", "ShelfName");

        public ActionResult MedicinesList()
        {
            return View(GetAllMedicines());
        }

        public ActionResult NewMedicine()
        {
            using (PharmAssistantContext db = new PharmAssistantContext())
            {
                ViewBag.Manufacturers = Manufacturers; // new SelectList(db.Manufacturers.ToList(), "ManufacturerId", "Name");
                ViewBag.Categories = Categories; // new SelectList(db.MedicineCategories.ToList(), "CategoryId", "Name");
                ViewBag.Suppliers = Suppliers; // new SelectList(db.Suppliers.ToList(), "SupplierId", "Name");
                ViewBag.Shelves = Shelves; // new SelectList(db.Shelves.ToList(), "ShelfId", "ShelfName");
            }

            return View(new Medicine());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddMedicine(Medicine medicine)
        {
            try
            {
                using (PharmAssistantContext db = new PharmAssistantContext())
                {
                    db.Medicines.Add(medicine);
                    db.SaveChanges();

                    return RedirectToAction("MedicinesList");
                }
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "Unable to add medicine. Try again later.";
                return View("NewMedicine", medicine);
                throw;
            }                
        }

        public ActionResult EditMedicine(int MedicineId)
        {
            using (PharmAssistantContext db = new PharmAssistantContext())
            {
                ViewBag.Manufacturers = Manufacturers; // new SelectList(db.Manufacturers.ToList(), "ManufacturerId", "Name");
                ViewBag.Categories = Categories; // new SelectList(db.MedicineCategories.ToList(), "CategoryId", "Name");
                ViewBag.Suppliers = Suppliers; // new SelectList(db.Suppliers.ToList(), "SupplierId", "Name");
                ViewBag.Shelves = Shelves; // new SelectList(db.Shelves.ToList(), "ShelfId", "ShelfName");

                return View(db.Medicines.Where(m => m.MedicineId == MedicineId).FirstOrDefault());
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateMedicine(Medicine medicine)
        {
            if(ModelState.IsValid)
            {
                try
                {
                    using (PharmAssistantContext db = new PharmAssistantContext())
                    {
                        db.Medicines.Attach(medicine);
                        db.Entry(medicine).State = EntityState.Modified;
                        db.SaveChanges();

                        return RedirectToAction("MedicinesList");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    ViewBag.ErrorMessage = "Unable to edit medicine.";
                    return View("EditMedicine", medicine);
                }
            }
            else
            {
                FillDropdowns();
                return View("EditMedicine", medicine);
            }
        }

        private void FillDropdowns()
        {
            ViewBag.Manufacturers = Manufacturers;
            ViewBag.Categories = Categories;
            ViewBag.Suppliers = Suppliers;
            ViewBag.Shelves = Shelves;
        }

        private ICollection<Medicine> GetAllMedicines()
        {
            try
            {
                using (PharmAssistantContext db = new PharmAssistantContext())
                {
                    return db.Medicines.Include("MedicineCategory").Include("Shelf").Include("Manufacturer").ToList();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public ActionResult MedicineCategories()
        {
            MedicineCategoryViewModel medicineData = new MedicineCategoryViewModel();

            medicineData.Category = new MedicineCategory();
            medicineData.CategoryList = GetAllCategories();            

            return View(medicineData);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddCategory(MedicineCategory category)
        {
            try
            {
                using (PharmAssistantContext db = new PharmAssistantContext())
                {
                    var existingCategory = db.MedicineCategories.Where(c => c.Name == category.Name).FirstOrDefault();

                    if (existingCategory == null)
                    {
                        db.MedicineCategories.Add(category);
                        db.SaveChanges();

                        return PartialView("_MedicineCategoryList", GetAllCategories());
                    }
                    else
                    {
                        return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Categoty Already Exists.");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, "Something went wrong. Please try again later.");
            }
        }

        public ActionResult DeleteCategory(long id)
        {
            try
            {
                using (PharmAssistantContext db = new PharmAssistantContext())
                {
                    MedicineCategory existingCategory = db.MedicineCategories.Where(c => c.CategoryId == id).FirstOrDefault();
                    db.MedicineCategories.Remove(existingCategory);
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return RedirectToAction("MedicineCategories");
        }

        public ActionResult ShelvesList()
        {
            ShelvesViewModel shelvesData = new ShelvesViewModel();
            shelvesData.Shelf = new Shelf();
            shelvesData.ShelfList = db.Shelves.ToList();

            return View("Shelves", shelvesData);
        }

        public ActionResult AddShelf(Shelf shelf)
        {
            try
            {
                using (PharmAssistantContext db = new PharmAssistantContext())
                {
                    var existingShelf = db.Shelves.Where(s => s.ShelfName == shelf.ShelfName && s.ShelfId == shelf.ShelfId).FirstOrDefault();

                    if (existingShelf == null)
                    {
                        db.Shelves.Add(shelf);
                        db.SaveChanges();

                        return PartialView("_ShelvesList", db.Shelves.ToList());
                    }
                    else
                    {
                        return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Shelf Already Exists.");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, "Something went wrong. Please try again later.");
            }
        }

        public ActionResult DeleteShelf(int id)
        {
            try
            {
                using (PharmAssistantContext db = new PharmAssistantContext())
                {
                    Shelf shelf = db.Shelves.Where(s => s.ShelfId == id).FirstOrDefault();
                    
                    db.Shelves.Remove(shelf);
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return RedirectToAction("ShelvesList");
        }

        private ICollection<MedicineCategory> GetAllCategories()
        {
            try
            {
                using (PharmAssistantContext db = new PharmAssistantContext())
                {
                    return db.MedicineCategories.ToList();
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