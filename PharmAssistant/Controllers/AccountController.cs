using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using PharmAssistant.Infrastructure;
using PharmAssistant.Models;
using PharmAssistant.Models.ViewModels;
using System.Collections.Generic;
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
        [ActionName("CreateNewUser")]
        public async Task<ActionResult> Create(CreateUserModel user)
        {
            if (ModelState.IsValid)
            {
                AppUser newUser = new AppUser { UserName = user.Name, Email = user.Email, City = user.City };
                IdentityResult result = await UserManager.CreateAsync(newUser, user.Password);

                if (result.Succeeded)
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

        public ActionResult EditUser(string id)
        {
            TempData["UserId"] = id;
            return RedirectToAction("EditUser");
        }

        public ActionResult EditUser()
        {
            string id = TempData["UserId"].ToString();
            return View();
        }

        public ActionResult DeleteUser(string id)
        {
            TempData["UserId"] = id;
            return RedirectToAction("DeleteUser");
        }

        public async Task<ActionResult> DeleteUser()
        {
            string id = TempData["UserId"].ToString();
            AppUser user = await UserManager.FindByIdAsync(id);

            if (user != null)
            {
                IdentityResult result = await UserManager.DeleteAsync(user);

                if (result.Succeeded)
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

        private void AddErrorsFromResult(IdentityResult result)
        {
            List<string> errors = new List<string>();

            foreach (string error in result.Errors)
            {
                errors.Add(error);
                //ModelState.AddModelError("", error);
            }

            ViewBag.Errors = errors;
        }
    }
}