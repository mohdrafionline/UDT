#region Using

using System;
using System.Data.Entity.Validation;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Security;
using Microsoft.Ajax.Utilities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using SmartAdminMvc.Models;
using SmartAdminMvc.App_Helpers;
using System.Security.Claims;
using Microsoft.Owin.Security;
using System.Web;


#endregion

namespace SmartAdminMvc.Controllers
{
    //[Authorize]
    public class AccountController : Controller
    {
        IAuthenticationManager Authentication
        {
            get { return HttpContext.GetOwinContext().Authentication; }
        }

        // TODO: This should be moved to the constructor of the controller in combination with a DependencyResolver setup
        // NOTE: You can use NuGet to find a strategy for the various IoC packages out there (i.e. StructureMap.MVC5)
        private readonly UserManager _manager = UserManager.Create();

        // GET: /account/forgotpassword
        [AllowAnonymous]
        public ActionResult ForgotPassword()
        {
            // We do not want to use any existing identity information
            EnsureLoggedOut();

            return View();
        }

        // GET: /account/login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            // We do not want to use any existing identity information
            EnsureLoggedOut();

            // Store the originating URL so we can attach it to a form field
            var viewModel = new AccountLoginModel { ReturnUrl = returnUrl };

            return View(viewModel);
        }

        // POST: /account/login
        [HttpPost]
        [AllowAnonymous]
        //public async Task<ActionResult> Login(AccountLoginModel viewModel)
        public ActionResult Login(AccountLoginModel viewModel)
        {
            // Ensure we have a valid viewModel to work with
            if (ModelState.IsValid)
            {
                DBEntity db = new DBEntity();
                var user = (from userlist in db.tblUsers
                            where userlist.Email == viewModel.Email || userlist.UserName == viewModel.UserName
                            select new
                            {
                                userlist.Id,
                                userlist.UserName,
                                userlist.Email
                            }).ToList();
                if (user.FirstOrDefault() != null)
                {
                    var identity = new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, viewModel.Email), }, DefaultAuthenticationTypes.ApplicationCookie);

                    Authentication.SignIn(new AuthenticationProperties
                    {
                        IsPersistent = viewModel.RememberMe
                    }, identity);

                    // If the user came from a specific page, redirect back to it
                    return RedirectToLocal(viewModel.ReturnUrl);
                }
                else
                {
                    // No existing user was found that matched the given criteria
                    ModelState.AddModelError("", "Invalid username or password.");
                }

            }
            return View(viewModel);
        }

        // GET: /account/error
        [AllowAnonymous]
        public ActionResult Error()
        {
            // We do not want to use any existing identity information
            EnsureLoggedOut();

            return View();
        }

        // GET: /account/register
        [AllowAnonymous]
        public ActionResult Register()
        {
            // We do not want to use any existing identity information
            EnsureLoggedOut();

            return View(new AccountRegistrationModel());
        }

        // POST: /account/register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Register(AccountRegistrationModel viewModel)
        {
            try
            {
                using (var context = new DBEntity())
                {
                    var chkUser = (from s in context.tblUsers where s.UserName == viewModel.Username || s.Email == viewModel.Email select s).FirstOrDefault();


                    if (chkUser == null)
                    {
                        var keyNew = Helper.GeneratePassword(10);
                        var password = Helper.EncodePassword(viewModel.Password, keyNew);
                        viewModel.Password = password;
                        viewModel.CreateDate = DateTime.Now;
                        viewModel.ModifyDate = DateTime.Now;
                        //context.tblUsers.Add(viewModel);
                        context.SaveChanges();
                        ModelState.Clear();

                        var identity = new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, viewModel.Email), }, DefaultAuthenticationTypes.ApplicationCookie);

                        Authentication.SignIn(new AuthenticationProperties
                        {
                            IsPersistent = true
                        }, identity);

                        // If the user came from a specific page, redirect back to it
                        return RedirectToAction("index", "home");
                    }
                    ModelState.AddModelError("", "User Allredy Exixts");
                    return View(viewModel);
                }
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", "Some exception occured" + e);
                return View(viewModel);
            }

        }

        // POST: /account/Logout
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Logout()
        {
            // First we clean the authentication ticket like always
            Authentication.SignOut();

            // Second we clear the principal to ensure the user does not retain any authentication
            HttpContext.User = new GenericPrincipal(new GenericIdentity(string.Empty), null);

            // Last we redirect to a controller/action that requires authentication to ensure a redirect takes place
            // this clears the Request.IsAuthenticated flag since this triggers a new request
            return RedirectToLocal();
        }

        private ActionResult RedirectToLocal(string returnUrl = "")
        {
            // If the return url starts with a slash "/" we assume it belongs to our site
            // so we will redirect to this "action"
            if (!returnUrl.IsNullOrWhiteSpace() && Url.IsLocalUrl(returnUrl))
                return Redirect(returnUrl);

            // If we cannot verify if the url is local to our host we redirect to a default location
            return RedirectToAction("index", "home");
        }

        private void AddErrors(DbEntityValidationException exc)
        {
            foreach (var error in exc.EntityValidationErrors.SelectMany(validationErrors => validationErrors.ValidationErrors.Select(validationError => validationError.ErrorMessage)))
            {
                ModelState.AddModelError("", error);
            }
        }

        private void AddErrors(IdentityResult result)
        {
            // Add all errors that were returned to the page error collection
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private void EnsureLoggedOut()
        {
            // If the request is (still) marked as authenticated we send the user to the logout action
            if (Request.IsAuthenticated)
                Logout();
        }

        private async Task SignInAsync(IdentityUser user, bool isPersistent)
        {
            // Clear any lingering authencation data
            FormsAuthentication.SignOut();

            // Create a claims based identity for the current user
            var identity = await _manager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);

            // Write the authentication cookie
            FormsAuthentication.SetAuthCookie(identity.Name, isPersistent);
        }

        // GET: /account/login
        [AllowAnonymous]
        public ActionResult Lock(string returnUrl)
        {
            var viewModel = new LockscreenModel { Email = HttpContext.User.Identity.Name };
            //string email = HttpContext.User.Identity.Name;
            //viewModel.Email = email;
            // We do not want to use any existing identity information
            EnsureLoggedOut();

            // Store the originating URL so we can attach it to a form field
            //var viewModel = new AccountLoginModel { ReturnUrl = returnUrl };

            return View(viewModel);
        }

        // POST: /account/login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Lock(LockscreenModel viewModel)
        {
            // Ensure we have a valid viewModel to work with
            if (ModelState.IsValid)
            {
                DBEntity db = new DBEntity();
                var user = (from userlist in db.tblUsers
                            where userlist.Email == viewModel.Email
                            select new
                            {
                                userlist.Id,
                                userlist.UserName,
                                userlist.Email
                            }).ToList();
                if (user.FirstOrDefault() != null)
                {
                    var identity = new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, viewModel.Email), }, DefaultAuthenticationTypes.ApplicationCookie);

                    Authentication.SignIn(new AuthenticationProperties
                    {
                        IsPersistent = true
                    }, identity);

                    // If the user came from a specific page, redirect back to it
                    return RedirectToLocal(viewModel.ReturnUrl);
                }
                else
                {
                    // No existing user was found that matched the given criteria
                    ModelState.AddModelError("", "Invalid username or password.");
                }

            }
            return View(viewModel);
        }

    }
}