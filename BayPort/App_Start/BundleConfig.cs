using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;

namespace BayPortColombia.App_Start
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                "~/Scripts/jquery-3.3.1.js")
                );

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                "~/Scripts/jquery.validate*"));

            //bundles.Add(new ScriptBundle("~/assets/jquery").Include(
            //    "~/assets/js/jquery-3.1.0.min.js")
            //);
            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
             "~/js/bootstrap.js")
            );
            //bundles.Add(new ScriptBundle("~/assets/css/bootstrap").Include(
            // "~/assets/css/bootstrap.min.css", "~/assets/css/bootstrap-theme.min.css")
            //);
            bundles.Add(new ScriptBundle("~/css/bootstrap").Include(
             "~/css/bootstrap.css")
            );

        }
    }
}