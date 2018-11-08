using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using PharmAsistant.Infrastructure;
using PharmAsistant.Models;
using PharmAssistant.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace PharmAsistant.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private AppUserManager UserManager {
            get{
                return HttpContext.GetOwinContext().GetUserManager<AppUserManager>();
            }
        }
     
        // GET: Admin
        public ActionResult Index()
        {
            return View(UserManager.Users);
        }

        public ActionResult Create()
        {
            return View(new CreateUserModel());
        }

        [HttpPost]
        public async Task<ActionResult> Create(CreateUserModel user)
        {
            if(ModelState.IsValid)
            {
                AppUser newUser = new AppUser { UserName = user.Name, Email = user.Email, City = user.City };
                IdentityResult result = await UserManager.CreateAsync(newUser, user.Password);

                if(result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    AddErrorsFromResult(result);                    
                }
            }
            return View(user);
        }

        public ActionResult Edit(string id)
        {
            TempData["UserId"] = id;
            return RedirectToAction("EditUser");
        }

        public ActionResult EditUser()
        {
            string id = TempData["UserId"].ToString();
            return View();
        }


        public ActionResult Delete(string id)
        {
            TempData["UserId"] = id;
            return RedirectToAction("DeleteUser");
        }

        public async Task<ActionResult> DeleteUser()
        {
            string id = TempData["UserId"].ToString();
            AppUser user = await UserManager.FindByIdAsync(id);

            if(user != null)
            {
                IdentityResult result = await UserManager.DeleteAsync(user);

                if(result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return View("Error", result.Errors);
                }
            }
            else
            {
                return View("Error", new string[] { "User not found." });
            }
        }

        private void AddErrorsFromResult(IdentityResult result)
        {
            List<string> errors = new List<string>();

            foreach(string error in result.Errors)
            {
                errors.Add(error);
                //ModelState.AddModelError("", error);
            }

            ViewBag.Errors = errors;
        }
    }
}