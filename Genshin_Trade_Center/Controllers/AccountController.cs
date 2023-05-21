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
    /// <summary>
    /// Authorize only Controller responsible for managing all account requests
    /// made by the website. 
    /// </summary>
    /// <remarks></remarks>
    [Authorize]
    public class AccountController : BaseController
    {

        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="Genshin_Trade_Center.Controllers.AccountController" /> class. 
        /// </summary>
        /// <remarks></remarks>
        public AccountController() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Genshin_Trade_Center.Controllers.AccountController" /> class. 
        /// </summary>
        /// <param name="userManager"> The User Manager</param>
        /// <param name="signInManager"> The Sign in Manager</param>
        /// <remarks></remarks>
        public AccountController(ApplicationUserManager userManager,
                    ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        /// <summary>
        /// Gets or sets the Sign in Manager.
        /// </summary>
        /// <value>The Sign in Manager if not null, otherwise
        /// return a new Sign in Manager</value>
        /// <remarks></remarks>
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

        /// <summary>
        /// Gets or sets the User Manager.
        /// </summary>
        /// <value>The User Manager not null, otherwise
        /// return a new User Manager</value>
        /// <remarks></remarks>
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
        /// <summary>
        /// Gets the Index View of the Account,
        /// containing basic user Information.
        /// </summary>
        /// <returns>
        /// A View Containing the Email and
        /// NickName of the Current User.
        /// </returns>
        /// <remarks></remarks>
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
        /// <summary>
        /// Returns a View containing the login and register forms.
        /// Redirects to the Index if the User is already logged in.
        /// Allows Anonymous Requests.
        /// </summary>
        /// <param name="returnUrl">
        /// The Url to return to after a successful login.
        /// </param>
        /// <returns>
        /// The Login View with an option to login or register.
        /// </returns>
        /// <remarks></remarks>
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
        /// <summary>
        /// Post Method which logs the user in or
        /// registers a new user based on the form submited.
        /// if successful redirects to the return Url.
        /// </summary>
        /// <param name="model">
        /// Model representing the data on the Login View.
        /// </param>
        /// <param name="returnUrl">
        /// The Url to return to after the user logs in (or registers).
        /// </param>
        /// <param name="submit">
        /// String containing the information on which form
        /// was sumbitted.
        /// </param>
        /// <returns>
        /// The View represented by the return Url if successful,
        /// otherwise the Login View with Added Errors.
        /// </returns>
        /// <remarks></remarks>
        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> Login
                    (AccountViewModel model, string returnUrl,
            string submit)
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
        /// <summary>
        /// Logs the User out of the website and
        /// redirects to Home.
        /// </summary>
        /// <returns>
        /// The Home View.
        /// </returns>
        /// <remarks></remarks>
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

            await SignInManager.SignInAsync(user,
                isPersistent: false, rememberBrowser: false);
            return RedirectToLocal(returnUrl);
        }
        #endregion
    }
}
