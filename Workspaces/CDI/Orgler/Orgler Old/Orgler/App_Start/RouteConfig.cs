using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Orgler
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "NewAccount",
                url: "NewAccount/{*catchall}",
                defaults: new { controller = "Home", action = "NewAccount" }
            );
            routes.MapRoute(
                name: "TopAccount",
                url: "TopAccount/{*catchall}",
                defaults: new { controller = "Home", action = "TopAccount" }
            );           
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Login", action = "Index", id = UrlParameter.Optional }
                );
            routes.MapRoute(
                name: "Login",
                url: "{controller}/{action}",
                defaults: new { controller = "Login", action = "Index" }
                );
           
        }
    }
}
