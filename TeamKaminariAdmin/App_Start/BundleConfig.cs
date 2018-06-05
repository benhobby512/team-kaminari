﻿using System.Web;
using System.Web.Optimization;

namespace TeamKaminariAdmin
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js",
                        "~/Scripts/jquery.unobtrusive-ajax.js",
                        "~/Scripts/jquery.unobtrusive*",
                        "~/Scripts/tablesorter/jquery.tablesorter.combined.js",
                        "~/Scripts/kaminari.js",
                        "~/Scripts/jquery.validate.unobtrusive.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/bootstrap-datepicker.js",
                      "~/Scripts/bootstrap-datetimepicker.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap/bootstrap.css",
                      "~/Content/bootstrap-datepicker.css",
                      "~/Content/bootstrap-datepicker.standalone.css",
                      "~/Content/bootstrap-datepicker3.css",
                      "~/Content/bootstrap-datepicker3.standalone.css",
                      "~/Content/bootstrap-theme.css",
                      "~/Content/bootstrap-datetimepicker.css",
                      "~/Content/style.css",
                      "~/Content/site.css"));
        }
    }
}