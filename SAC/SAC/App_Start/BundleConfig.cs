using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;

namespace SAC.App_Start
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {

            bundles.Add(new ScriptBundle("~/Scripts/Login").Include(
                 "~/Metronic/assets/global/plugins/jquery-migrate.min.js",
                 "~/Metronic/assets/global/plugins/bootstrap/js/bootstrap.min.js",
                 "~/Metronic/assets/global/plugins/jquery.blockui.min.js",
                 "~/Metronic/assets/global/plugins/jquery.cokie.min.js",
                 "~/Metronic/assets/global/plugins/uniform/jquery.uniform.min.js",
                 "~/Metronic/assets/global/plugins/jquery-validation/js/jquery.validate.min.js",
                 "~/Content/plugins/sweetalert/sweetalert.min.js",
                 "~/Metronic/assets/global/scripts/metronic.js",
                 "~/Metronic/assets/admin/layout/scripts/layout.js",
                 "~/Metronic/assets/admin/layout/scripts/demo.js",
                 "~/Metronic/assets/admin/pages/scripts/login.js",
                 "~/Content/js/SAC.js",
                 "~/Content/js/login.js")
            );

            bundles.Add(new StyleBundle("~/Styles/Login").Include(

                "~/Metronic/assets/global/plugins/simple-line-icons/simple-line-icons.min.css",
                "~/Metronic/assets/global/plugins/bootstrap/css/bootstrap.min.css",
                "~/Metronic/assets/global/plugins/uniform/css/uniform.default.css",
                "~/Content/plugins/sweetalert/sweetalert.css",
                "~/Metronic/assets/admin/pages/css/login.css",
                "~/Metronic/assets/global/css/components.css",
                "~/Metronic/assets/global/css/plugins.css",
                "~/Metronic/assets/admin/layout/css/layout.css",
                "~/Metronic/assets/admin/layout/css/themes/darkblue.css",
                "~/Metronic/assets/admin/layout/css/custom.css",
                "~/Content/css/SAC.css"
                )

            );

            bundles.Add(new ScriptBundle("~/Scripts/Default").Include(


                "~/Metronic/assets/global/plugins/bootstrap/js/bootstrap.min.js",
                "~/Metronic/assets/global/plugins/bootstrap-hover-dropdown/bootstrap-hover-dropdown.min.js",
                "~/Metronic/assets/global/plugins/jquery-slimscroll/jquery.slimscroll.min.js",
                "~/Metronic/assets/global/plugins/jquery.blockui.min.js",
                "~/Metronic/assets/global/plugins/jquery.cokie.min.js",
                "~/Metronic/assets/global/plugins/uniform/jquery.uniform.min.js",
                "~/Metronic/assets/global/plugins/bootstrap-switch/js/bootstrap-switch.min.js",
                "~/Metronic/assets/global/plugins/jquery-validation/js/jquery.validate.min.js",
                "~/Metronic/assets/global/plugins/jquery-validation/js/additional-methods.min.js",
                "~/Content/plugins/sweetalert/sweetalert.min.js",
                "~/Metronic/assets/global/plugins/select2/select2.min.js",
                "~/Metronic/assets/global/scripts/metronic.js",
                "~/Metronic/assets/admin/layout/scripts/layout.js",
                "~/Metronic/assets/admin/layout/scripts/quick-sidebar.js",
                "~/Metronic/assets/admin/layout/scripts/demo.js",
                "~/Metronic/assets/admin/pages/scripts/index.js",
                "~/Metronic/assets/admin/pages/scripts/tasks.js",
                "~/Content/js/SAC.js"
                ));

            bundles.Add(new StyleBundle("~/Styles/Default").Include(
                "~/Metronic/assets/global/plugins/bootstrap/css/bootstrap.min.css",
                "~/Metronic/assets/global/plugins/uniform/css/uniform.default.css",
                "~/Metronic/assets/global/plugins/bootstrap-switch/css/bootstrap-switch.min.css",
                "~/Metronic/assets/global/plugins/bootstrap-daterangepicker/daterangepicker-bs3.css",
                "~/Metronic/assets/global/plugins/fullcalendar/fullcalendar.min.css",
                "~/Metronic/assets/global/plugins/jqvmap/jqvmap/jqvmap.css",
                "~/Content/plugins/sweetalert/sweetalert.css",
                "~/Metronic/assets/global/plugins/select2/select2.css",
                "~/Metronic/assets/admin/pages/css/tasks.css",
                "~/Metronic/assets/global/css/components.css",
                "~/Metronic/assets/global/css/plugins.css",
                "~/Metronic/assets/admin/layout/css/layout.css",
                "~/Metronic/assets/admin/layout/css/themes/darkblue.css",
                "~/Metronic/assets/admin/layout/css/custom.css",
                "~/Content/css/SAC.css"
            ));



            BundleTable.EnableOptimizations = true;
        }
    }
}