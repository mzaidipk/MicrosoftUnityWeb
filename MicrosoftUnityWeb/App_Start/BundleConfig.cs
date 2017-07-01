using System.Web;
using System.Web.Optimization;

namespace MicrosoftUnityWeb
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

            bundles.Add(new ScriptBundle("~/Bundle/BaseLibs")
                .Include("~/Scripts/jquery-ui-1.12.1.js")
                .Include("~/Scripts/jquery-ui-timepicker-addon.js")
                .Include("~/Scripts/jquery.blockUI.js")
                .Include("~/Scripts/knockout-3.4.2.js")
                .Include("~/Scripts/knockout.mapping-latest.js")
                .Include("~/Scripts/knockout.validation.js")
                .Include("~/Scripts/underscore.js")
                .Include("~/Scripts/underscore-ko.js")
                .Include("~/Scripts/moment.js")
                .Include("~/Scripts/toastr.js")
                .Include("~/Scripts/amplify.js")
                .Include("~/Scripts/knockout-sortable.js")
                .Include("~/Scripts/require.js")
                .Include("~/Scripts/respond.js")
                .Include("~/Scripts/App/architecture.js")
                .Include("~/Scripts/App/requireConfig.js")
                );


            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js"));

            bundles.Add(new StyleBundle("~/Content/css")
                .Include("~/Content/bootstrap.css")
                .Include("~/Content/site.css")
                .Include("~/Content/CSS/toastr.css")
                .Include("~/Content/themes/base/all.css")
                      );
        }
    }
}
