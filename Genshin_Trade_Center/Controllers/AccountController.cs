using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Genshin_Trade_Center.Models;
using System.Net;

namespace Genshin_Trade_Center.Controllers
{
    [Authorize]
    public class AccountController : BaseController
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

        public AccountController() {}

        public AccountController(ApplicationUserManager userManager,
            ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext()
                    .Get<ApplicationSignInManager>();
            }
            private set 
            { 
                _signInManager = value; 
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext()
                    .GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        // GET: /Account/Index
        public ActionResult Index()
        {
            User user = UserManager
                .FindById(User.Identity.GetUserId());

            ManageViewModel model = new ManageViewModel
            {
                Email = user.Email,
                Nickname = user.UserName
            };
            return View(model);
        }

        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            if (Request.IsAuthenticated)
            {
                return RedirectToAction("Index");
            }

            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> Login
            (AccountViewModel model,string returnUrl, string submit)
        {
            if (!ModelState.IsValid)
            {
                return new 
                    HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if (submit == "Log in")
            {
                return await 
                    PasswordLogin(model, returnUrl);
            }
            return await
                Register(model, returnUrl);
        }

        // POST: /Account/LogOff
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut
                (DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("Index", "Home");
        }

        protected override void Dispose(bool disposing)
        {
            if (!disposing)
            {
                base.Dispose(disposing);
                return;
            }

            if (_userManager != null)
            {
                _userManager.Dispose();
                _userManager = null;
            }
            if (_signInManager != null)
            {
                _signInManager.Dispose();
                _signInManager = null;
            }
            base.Dispose(disposing);
        }

        #region Helpers

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (string error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }

        private async Task<ActionResult> PasswordLogin
            (AccountViewModel model, string returnUrl)
        {
            SignInStatus resultLogin = await SignInManager
                .PasswordSignInAsync(model.LoginViewModel.UserName,
                model.LoginViewModel.Password, false,
                shouldLockout: false);

            if (resultLogin != SignInStatus.Success)
            {
                ModelState.AddModelError
                        ("", "Invalid login attempt.");
                return View(model);
            }
            return RedirectToLocal(returnUrl);
        }

        private async Task<ActionResult> Register
            (AccountViewModel model, string returnUrl)
        {
            User user = new User
            {
                UserName = model.RegisterViewModel.UserName,
                Email = model.RegisterViewModel.Email
            };
            IdentityResult resultregister = await UserManager
                .CreateAsync(user, model.RegisterViewModel.Password);

            if (!resultregister.Succeeded)
            {
                AddErrors(resultregister);
                return View(model);
            }

            await SignInManager .SignInAsync(user,
                isPersistent: false, rememberBrowser: false);
            return RedirectToLocal(returnUrl);
        }
        #endregion
    }
}
