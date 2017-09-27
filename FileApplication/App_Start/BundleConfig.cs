using System.Web;
using System.Web.Optimization;

namespace FileApplication
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

            // js tree plugin
            bundles.Add(new ScriptBundle("~/bundles/jstree").Include(
                      "~/Scripts/jstree-master/dist/jstree.js"));
            bundles.Add(new StyleBundle("~/Content/jstree").Include(
                      "~/Scripts/jstree-master/dist/themes/default/style.css"));

            // js-Filer plugin
            bundles.Add(new ScriptBundle("~/bundles/filer").Include(
                      "~/Scripts/js-filer-master/jsfiler.js"));
            bundles.Add(new StyleBundle("~/Content/filer").Include(
                      "~/Scripts/js-filer-master/jsfiler.css"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));
        }
    }
}
