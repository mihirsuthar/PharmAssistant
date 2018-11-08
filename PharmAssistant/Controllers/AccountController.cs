using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using PharmAsistant.Infrastructure;
using PharmAsistant.Models;
using PharmAssistant.Infrastructure;
using PharmAssistant.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace PharmAsistant.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private AppUserManager UserManager{
            get{
                return HttpContext.GetOwinContext().GetUserManager<AppUserManager>();
            }
        }

        private IAuthenticationManager AuthManager {
            get {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        [AllowAnonymous]
        // GET: Account
        public ActionResult Login(string returnUrl)
        {
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                return View("Error", new string[] { "Access Denied" });
            }

            ViewBag.ReturnUrl = returnUrl;
            return View(new LoginModel());
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginModel details, string returnUrl)
        {
            AppUser user = await UserManager.FindAsync(details.Name, details.Password);
            if(user == null)
            {
                ModelState.AddModelError("", "Invalid Username or Password.");
            }
            else
            {
                ClaimsIdentity identity = await UserManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
                identity.AddClaims(LocationClaimsProvider.GetClaims(identity));
                identity.AddClaims(ClaimsRoles.CreateRolesFromClaims(identity));
                AuthManager.SignOut();
                AuthManager.SignIn(new AuthenticationProperties { IsPersistent = false }, identity);

                return Redirect(returnUrl);
            }

            ViewBag.returnUrl = returnUrl;
            return View(details);
        }

        [Authorize]
        public ActionResult Logout()
        {
            AuthManager.SignOut();
            return RedirectToAction("Index", "Home");
        }
    }
}