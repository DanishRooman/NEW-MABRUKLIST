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

            bundles.Add(new StyleBundle("~/Content/admin.css").Include(
                   "~/Content/bootstrap.min.css",
                   "~/StyleSheet/metisMenu.min.css",
                   "~/StyleSheet/sb-admin-2.css",
                   "~/StyleSheet/morris.css",
                   "~/fonts/font-awesome.min.css",
                   "~/Content/jquery.toast.min.css",
                   "~/Content/jquery-confirm.css",
                   "~/Content/components.css"
                 ));

            bundles.Add(new StyleBundle("~/Content/Datatable.css").Include(
                "~/Content/dataTables.bootstrap.min.css",
                "~/Content/responsive.bootstrap.min.css"
                ));

            bundles.Add(new ScriptBundle("~/Scripts/Datatable.js").Include(
                "~/Scripts/jquery.dataTables.min.js",
                "~/Scripts/dataTables.bootstrap.min.js",
                "~/Scripts/dataTables.responsive.min.js",
                "~/Scripts/responsive.bootstrap.min.js"
                ));

            bundles.Add(new ScriptBundle("~/Scripts/BlockUI.js").Include(
               "~/javascript/jquery.blockui.min.js",
               "~/javascript/app.js",
               "~/javascript/ui-blockui.js"
               ));

            bundles.Add(new ScriptBundle("~/Scripts/admin.js").Include(
              "~/Scripts/jquery-1.10.2.min.js",
              "~/Scripts/Base.js",
              "~/Scripts/jquery.unobtrusive-ajax.js",
              "~/Scripts/jquery.validate.min.js",
              "~/javascript/bootstrap.min.js"
              ));

            bundles.Add(new ScriptBundle("~/Scripts/unobtrusive").Include(
                "~/Scripts/jquery.validate.unobtrusive.min.js"
                ));


            bundles.Add(new ScriptBundle("~/Scripts/toast.js").Include(
                "~/Scripts/jquery.toast.min.js"
                ));

            bundles.Add(new ScriptBundle("~/Scripts/jquery-confirm.js").Include(
                "~/javascript/jquery-confirm.js"
                ));

            bundles.Add(new ScriptBundle("~/Scripts/sb-admin-2.js").Include(
                "~/javascript/metisMenu.min.js",
                "~/javascript/sb-admin-2.js"
                ));

            bundles.Add(new StyleBundle("~/Scripts/component.css").Include(
                           "~/Content/components.css"
                           ));
        }
    }
}
