using System.Web;
using System.Web.Optimization;

namespace SMT.SpotRental.UI
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            //========================================================================================//
            //------------------------Javascript Bundels---------------------------------------------//

            //==============================Common JS====================================================//
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include("~/Scripts/jquery/jquery-{version}.js"));
            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include("~/Scripts/jquery/jquery.validate*"));
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include("~/Scripts/bootstrap/modernizr-*"));
            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include("~/Scripts/bootstrap/bootstrap.js","~/Scripts/bootstrap/respond.js", "~/Scripts/bootstrap/bootstrap-toggle.min.js"));
            bundles.Add(new ScriptBundle("~/bundles/BS_CAL").Include("~/Scripts/bootstrap/bootstrap-datepicker.js"));
            bundles.Add(new ScriptBundle("~/bundles/dataTables").Include("~/Scripts/bootstrap/dataTables.bootstrap.min.js"));
            bundles.Add(new ScriptBundle("~/bundles/jquery/dataTables").Include("~/Scripts/jquery/jquery.dataTables.min.js"));
            //==============================Page JS==================================================//

            bundles.Add(new ScriptBundle("~/Jquery/Pages").Include("~/Scripts/pages/login.js"));
            bundles.Add(new ScriptBundle("~/Jquery/Common").Include("~/Scripts/pages/common.js"));
            bundles.Add(new ScriptBundle("~/Jquery/DashBoard").Include("~/Scripts/pages/dashboard.js"));
            bundles.Add(new ScriptBundle("~/Jquery/AdhocReq").Include("~/Scripts/pages/adhocreq.js"));
            bundles.Add(new ScriptBundle("~/Jquery/adhochistory").Include("~/Scripts/pages/adhochistory.js"));
            bundles.Add(new ScriptBundle("~/Jquery/vendorbucket").Include("~/Scripts/pages/vendorbucket.js"));
            bundles.Add(new ScriptBundle("~/Jquery/supervisorbucket").Include("~/Scripts/pages/supervisorbucket.js"));
            bundles.Add(new ScriptBundle("~/Jquery/usertrip").Include("~/Scripts/pages/usertrip.js"));
            bundles.Add(new ScriptBundle("~/Jquery/profile").Include("~/Scripts/pages/profile.js"));
            bundles.Add(new ScriptBundle("~/Jquery/manageVendors").Include("~/Scripts/pages/manageVendors.js"));
            bundles.Add(new ScriptBundle("~/Jquery/manageVehicles").Include("~/Scripts/pages/manageVehicles.js"));
            bundles.Add(new ScriptBundle("~/Jquery/manageDriverGuard").Include("~/Scripts/pages/manageDriverGuard.js"));
            bundles.Add(new ScriptBundle("~/Jquery/uploadDocuments").Include("~/Scripts/pages/uploadDocuments.js"));
            bundles.Add(new ScriptBundle("~/Jquery/manageRole").Include("~/Scripts/pages/manageRole.js"));            
            bundles.Add(new ScriptBundle("~/Jquery/manageUsersList").Include("~/Scripts/pages/manageUsersList.js"));
            bundles.Add(new ScriptBundle("~/Jquery/manageMenu").Include("~/Scripts/pages/manageMenu.js"));
            bundles.Add(new ScriptBundle("~/Jquery/sweetalert").Include("~/Scripts/plugins/sweetalert/sweetalert.js"));

            //---------------------EOF: Javascript Bundels-------------------------------------//

            //       **                   ***              **              **



            //========================================================================================//
            //--------------------------------Style Sheet Bundels-------------------------------------//
            bundles.Add(new StyleBundle("~/Content/bootstrap").Include("~/Content/bootstrap/bootstrap.css",
                      "~/Content/bootstrap/bootstrap-min.css","~/Content/bootstrap/bootstrap-datetimepicker.css",
                      "~/Content/bootstrap/bootstrap-dialog.min.css","~/Content/bootstrap/bootstrap-toggle.min.css",
                      "~/Content/bootstrap/responsive.css","~/Content/bootstrap/datepicker3.css"));

            bundles.Add(new StyleBundle("~/Content/AdminLTE").Include("~/Content/bootstrap/AdminLTE.css"));
            bundles.Add(new StyleBundle("~/Content/font-awesome").Include("~/Content/bootstrap/font-awesome.min.css"));
            bundles.Add(new StyleBundle("~/Content/login").Include("~/Content/pages/login.css"));
            bundles.Add(new StyleBundle("~/Content/bucket").Include("~/Content/pages/bucket.css"));
            bundles.Add(new StyleBundle("~/Content/loader").Include("~/Content/pages/loader.css"));
            bundles.Add(new StyleBundle("~/Content/addons").Include("~/Content/pages/addons.css"));
            bundles.Add(new StyleBundle("~/Content/dataTables").Include("~/Content/bootstrap/dataTables.bootstrap.min.css"));
            bundles.Add(new StyleBundle("~/Content/sweetalert").Include("~/Content/plugins/sweetalert.css"));

            //---------------------EOF: Style Sheet Bundels-------------------------------------//
        }
    }
}
