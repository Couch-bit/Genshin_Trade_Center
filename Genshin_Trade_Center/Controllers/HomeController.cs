using System.Web.Mvc;

namespace Genshin_Trade_Center.Controllers
{
    /// <summary>
    /// Controller for the home page.
    /// </summary>
    /// <remarks></remarks>
    public class HomeController : BaseController
    {
        /// <summary>
        /// Returns the the Home page.
        /// </summary>
        /// <returns>
        /// The Home page.
        /// </returns>
        /// <remarks></remarks>
        public ActionResult Index()
        {
            return View();
        }
    }
}
