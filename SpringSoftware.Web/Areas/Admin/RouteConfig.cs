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
                "Admin_News",
                "admin/News/{action}",
                new { controller = "News", action = "Index", area = "Admin" }
                );

            context.MapRoute(
    "Admin_NewsTypes",
    "admin/NewsTypes/{action}",
    new { controller = "NewsTypes", action = "Index", area = "Admin" }
    );



        }
    }
}