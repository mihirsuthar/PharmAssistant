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
    public class MedicineController : Controller
    {
        // GET: Medicine
        
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