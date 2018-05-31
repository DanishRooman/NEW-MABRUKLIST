using System.Web;
using System.Web.Optimization;

namespace ColidColorlib
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

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css"
                   ));
            bundles.Add(new StyleBundle("~/Content/main").Include(
           "~/StyleSheet/normalize.css",
           "~/StyleSheet/style.css",
           "~/StyleSheet/responsive.css"
                ));
            bundles.Add(new StyleBundle("~/Content/plugins").Include(
                     "~/StyleSheet/style.css",
                     "~/StyleSheet/owl.carousel.min.css",
                     "~/StyleSheet/themify-icons.css",
                     "~/StyleSheet/magnific-popup.css",
                     "~/StyleSheet/animate.css"
                  ));

            //Admin page Css

            bundles.Add(new StyleBundle("~/Content/adminCss").Include(
                   "~/Content/bootstrap.min.css",
                   "~/StyleSheet/metisMenu.min.css",
                   "~/StyleSheet/sb-admin-2.css",
                   "~/StyleSheet/morris.css",
                   "~/fonts/font-awesome.min.css"
                 ));

        }
    }
}
