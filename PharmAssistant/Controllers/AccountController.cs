using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using PharmAssistant.Infrastructure;
using PharmAssistant.Models;
using PharmAssistant.Models.ViewModels;
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

        // GET: Admin
        public ActionResult Index()
        {
            UserViewModel userData = new UserViewModel();
            userData.User = new CreateUserModel();
            userData.UsersList = UserManager.Users;

            return View(userData);
        }

        //public ActionResult Create()
        //{
        //    return View(new CreateUserModel());
        //}

        [HttpPost]        
        //public async Task<ActionResult> CreateUser(CreateUserModel user)
        public ActionResult CreateUser(CreateUserModel user)
        {
            //IQueryable<AppUser> users = null;
            if (ModelState.IsValid)
            {
                AppUser newUser = new AppUser { UserName = user.Name, PhoneNumber = user.PhoneNumber.ToString(), Email = user.Email, City = user.City };
                IdentityResult result = UserManager.Create(newUser, user.Password);
                
                if (result.Succeeded)
                {
                    //users = UserManager.Users;
                    ViewBag.SuccessMessage = "User Created Successfully.";
                }
                else
                {
                    ViewBag.ErrorMessage = "Unable to create user.";
                }                
            }

            //return RedirectToAction("Index");
            return PartialView("_UsersList", GetAllUsers());
        }

        public ActionResult EditUser(string id)
        {
            UserViewModel userData = new UserViewModel(); ;

            using (PharmAssistantContext db = new PharmAssistantContext())
            {
                AppUser user = UserManager.FindById(id);
                userData.User = new CreateUserModel { Name = user.UserName, PhoneNumber = long.Parse(user.PhoneNumber), Email = user.Email, City = user.City };
                userData.UsersList = UserManager.Users;                
            }

            ViewBag.UserId = id;

            return View("Index", userData);
            
        }

        public ActionResult UpdateUser(CreateUserModel user, string UserId)
        {
            AppUser updateUser = UserManager.FindById(UserId);

            if(updateUser != null)
            {
                updateUser.Email = user.Email;
                updateUser.PhoneNumber = user.PhoneNumber.ToString();

                IdentityResult result = UserManager.Update(updateUser);

                if (result.Succeeded)
                {   
                    return Json(new { RedirectUrl = Url.Action("Index") });
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

        public ActionResult DeleteUser(string id)
        {   
            AppUser user = UserManager.FindById(id);

            if (user != null)
            {
                IdentityResult result = UserManager.Delete(user);

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
                //ModelState.AddModelError("", "Invalid Username or Password.");
                ViewBag.AuthError = "Invalid Username or Password.";
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

        private IQueryable<AppUser> GetAllUsers()
        {
            return UserManager.Users;
        }
    }
}