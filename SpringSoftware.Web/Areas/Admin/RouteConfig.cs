using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SpringSoftware.Web.Areas.Admin
{
    internal static class RouteConfig
    {
        internal static void RegisterRoutes(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Admin_Default",
                "Admin/{controller}/{action}/{id}",
                new {controller = "News", action = "Index", area = "Admin", id = ""},
                new[] { "SpringSoftware.Web.Areas.Admin.Controllers" });

            //context.MapRoute(
            //    "Admin_News",
            //    "admin/News/{action}",
            //    new { controller = "News", action = "Index", area = "Admin" });

            //context.MapRoute(
            //    "Admin_NewsTypes",
            //    "admin/NewsTypes/{action}",
            //    new { controller = "NewsTypes", action = "Index", area = "Admin" });


            //context.MapRoute(
            //    "Admin_Account",
            //    "admin/Account/{action}",
            //    new { controller = "Account", action = "Login", area = "Admin" });

            //context.MapRoute(
            // "Admin_Manage",
            // "admin/Manage/{action}",
            // new { controller = "Manage", action = "Index", area = "Admin" });

            //context.MapRoute(
            //    "Admin_RolesAdmin",
            //    "admin/RolesAdmin/{action}",
            //    new { controller = "RolesAdmin", action = "Login", area = "Admin" });

            //context.MapRoute(
            // "Admin_UsersAdmin",
            // "admin/UsersAdmin/{action}",
            // new { controller = "UsersAdmin", action = "Index", area = "Admin" });

            //context.MapRoute(
            //"Admin_Comments",
            //"admin/Comments/{action}",
            // new { controller = "Comments", action = "Index", area = "Admin" });
        }
    }
}