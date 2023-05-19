using System.Globalization;
using System.Threading;
using System.Web.Mvc;

namespace Genshin_Trade_Center.Controllers
{
    public class BaseController : Controller
    {
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
