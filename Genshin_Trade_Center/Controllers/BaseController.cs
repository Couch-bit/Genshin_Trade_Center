using System.Globalization;
using System.Net;
using System.Threading;
using System.Web.Mvc;

namespace Genshin_Trade_Center.Controllers
{
    /// <summary>
    /// The controller which is inherited by 
    /// all other controllers in the application. 
    /// </summary>
    /// <remarks></remarks>
    public class BaseController : Controller
    {
        /// <summary>
        /// Ensures the <see cref="CultureInfo" /> 
        /// is european every time an action is executed.
        /// </summary>
        /// <param name="filterContext">
        /// the action executing context.
        /// </param>
        /// <remarks></remarks>
        protected override void OnActionExecuting
            (ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);
            CultureInfo cultureInfo = CultureInfo.GetCultureInfo("eu");
            Thread.CurrentThread.CurrentCulture = cultureInfo;
            Thread.CurrentThread.CurrentUICulture = cultureInfo;
        }

        /// <summary>
        /// Called when an unhandled exception occurs in an action.
        /// Sets the context result to HTTP 500.
        /// </summary>
        /// <param name="filterContext">
        /// Information about the current request and action.
        /// </param>
        protected override void OnException
                    (ExceptionContext filterContext)
        {
            filterContext.Result = new
                HttpStatusCodeResult
                (HttpStatusCode.InternalServerError);
            filterContext.ExceptionHandled = true;
        }
    }
}
