using System.Web;
using System.Web.Optimization;

namespace Shopper
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/js").Include(
                      "~/Scripts/jquery-1.11.3.min.js",
                      "~/Scripts/jquery-ui.js",
                      "~/AssetsClient/nivo-slider/jquery.nivo.slider.js",
                      "~/Scripts/bootstrap-3.3.6-dist/js/bootstrap.min.js",
                      "~/AssetsClient/js/move-top.js",
                      "~/AssetsClient/js/easing.js",
                      "~/AssetsClient/js/startstop-slider.js"
                      ));

            bundles.Add(new ScriptBundle("~/bundles/CustumJS").Include(
                    "~/AssetsClient/js/Controller/baseController.js",
                    "~/AssetsClient/js/Controller/AlertInfo.js"
                ));

            bundles.Add(new StyleBundle("~/bundles/css").Include(
                      "~/Scripts/bootstrap-3.3.6-dist/css/bootstrap.min.css",
                      "~/Scripts/bootstrap-3.3.6-dist/css/bootstrap-theme.min.css",
                      "~/AssetsClient/css/jquery-ui.css",
                      "~/AssetsClient/nivo-slider/themes/default/default.css",
                      "~/AssetsClient/nivo-slider/nivo-slider.css",
                      "~/AssetsClient/css/style.css",
                      "~/AssetsClient/css/slider.css"
                      ));

            BundleTable.EnableOptimizations = false;
        }
    }
}
