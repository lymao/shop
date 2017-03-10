using System.Web;
using System.Web.Optimization;

namespace Web
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {


            bundles.Add(new ScriptBundle("~/css/base").Include(
                        "~/Assets/client/css/bootstrap.css",
                        "~/Assets/admin/libs/jquery-ui/themes/black-tie/jquery-ui.min.css",
                        "~/Assets/client/css/etalage.css",
                        "~/Assets/client/css/custom.css",
                        "~/Assets/client/css/style.css"
                        ));


            bundles.Add(new ScriptBundle("~/js/plugins").Include(
                      "~/Assets/admin/libs/jquery-ui/jquery-ui.min.js",
                      "~/Assets/client/js/jquery.etalage.min.js",
                      "~/Assets/admin/libs/mustache/mustache.js",
                      "~/Assets/admin/libs/numeral/numeral.js",
                      "~/Assets/admin/libs/jquery-validation/dist/jquery.validate.js",
                      "~/Assets/admin/libs/jquery-validation/dist/additional-methods.js",
                      "~/Assets/client/js/controller/shoppingCart.js",
                      "~/Assets/client/js/common.js",
                      "~/Assets/client/js/jquery.flexisel.js"
                      ));

            //// Use the development version of Modernizr to develop with and learn from. Then, when you're
            //// ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            //bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
            //            "~/Scripts/modernizr-*"));

            //bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
            //          "~/Scripts/bootstrap.js",
            //          "~/Scripts/respond.js"));

            //bundles.Add(new StyleBundle("~/Content/css").Include(
            //          "~/Content/bootstrap.css",
            //          "~/Content/site.css"));
        }
    }
}
