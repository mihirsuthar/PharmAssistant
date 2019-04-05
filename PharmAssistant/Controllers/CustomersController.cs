using PharmAssistant.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PharmAssistant.Controllers
{
    public class CustomersController : Controller
    {
        // GET: Customers
        public ActionResult Index()
        {
            try
            {
                using (PharmAssistantContext db = new PharmAssistantContext())
                {
                    if (TempData["SuccessMessage"] != null)
                    {
                        ViewBag.SuccessMessage = TempData["SuccessMessage"];
                    }
                    if (TempData["ErrorMessage"] != null)
                    {
                        ViewBag.ErrorMessage = TempData["ErrorMessage"];
                    }
                    return View(db.Customers.ToList());
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return View();
            }
        }

        public ActionResult NewCustomer()
        {
            return View(new Customer());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddCustomer(Customer customer)
        {
            if(ModelState.IsValid)
            {
                try
                {
                    using (PharmAssistantContext db = new PharmAssistantContext())
                    {
                        db.Customers.Add(customer);
                        db.SaveChanges();

                        TempData["SuccessMessage"] = "Customer added successfully";
                        return RedirectToAction("Index");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    TempData["ErrorMessage"] = "Unable to add customer. Please try again later.";
                    return RedirectToAction("Index");
                }
            }
            else
            {
                return View("NewCustomer", customer);
            }
            
        }

        public ActionResult EditCustomer(int CustomerId)
        {
            try
            {
                using (PharmAssistantContext db = new PharmAssistantContext())
                {                    
                    return View(db.Customers.Where(c => c.CustomerId == CustomerId).FirstOrDefault());
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                TempData["ErrorMessage"] = "Unable to edit customer. Please try again later.";
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateCustomer(Customer customer)
        {
            try
            {
                using (PharmAssistantContext db = new PharmAssistantContext())
                {
                    db.Customers.Attach(customer);
                    db.Entry(customer).State = EntityState.Modified;
                    db.SaveChanges();

                    TempData["SuccessMessage"] = "Customer updated successfully";
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                TempData["ErrorMessage"] = "Unable to update customer. Please try again later.";
                return RedirectToAction("Index");
            }
        }

        public ActionResult DeleteCustomer(int CustomerId)
        {
            try
            {
                using (PharmAssistantContext db = new PharmAssistantContext())
                {
                    db.Customers.Remove(db.Customers.Where(c => c.CustomerId == CustomerId).FirstOrDefault());
                    db.SaveChanges();

                    TempData["SuccessMessage"] = "Customer deleted successfully";
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                TempData["ErrorMessage"] = "Unable to delete customer. Please try again later.";
                return RedirectToAction("Index");
            }
        }
    }
}