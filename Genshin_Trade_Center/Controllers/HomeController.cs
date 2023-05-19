using System.Web.Mvc;

namespace Genshin_Trade_Center.Controllers
{
    /// <summary>
    /// Controller for the home page
    /// </summary>
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}