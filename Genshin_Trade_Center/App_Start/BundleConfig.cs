using System.Web.Optimization;

namespace Genshin_Trade_Center
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jqueryval")
                .Include("~/Scripts/jquery.validate*"));
        }
    }
}
