﻿using System.Globalization;
using System.Threading;
using System.Web.Mvc;

namespace Genshin_Trade_Center.Controllers
{
    /// <summary>
    /// The Controller which is inherited by 
    /// all other controllers in the application. 
    /// </summary>
    /// <remarks></remarks>
    public class BaseController : Controller
    {
        /// <summary>
        /// Ensures The Culture is european every time
        /// an action is executed.
        /// </summary>
        /// <param name="filterContext">
        /// the Action Executing Context
        /// </param>
        /// <remarks></remarks>
        protected override void OnActionExecuting(
                    ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);
            CultureInfo cultureInfo = CultureInfo.GetCultureInfo("eu");
            Thread.CurrentThread.CurrentCulture = cultureInfo;
            Thread.CurrentThread.CurrentUICulture = cultureInfo;
        }
    }
}
