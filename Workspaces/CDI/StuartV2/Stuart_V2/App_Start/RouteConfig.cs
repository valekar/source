using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Stuart_V2
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapMvcAttributeRoutes();

            routes.MapRoute(
                name: "Case",
                url: "case/{*catchall}",
                defaults: new { controller = "Home", action = "Case" }
            );
            routes.MapRoute(
                name: "Constituent",
                url: "constituent/{*catchall}",
                defaults: new { controller = "Home", action = "Constituent" }
            );
            routes.MapRoute(
                name: "Transaction",
                url: "transaction/{*catchall}",
                defaults: new { controller = "Home", action = "Transaction" }
            );
            routes.MapRoute(
                name: "Locator",
                url: "locator/{*catchall}",
                defaults: new { controller = "Home", action = "Locator" }
            );
             routes.MapRoute(
                name: "Account",
                url: "account/{*catchall}",
                defaults: new { controller = "Home", action = "Account" }
            );
             routes.MapRoute(
                name: "Upload",
                url: "upload/{*catchall}",
                defaults: new { controller = "Home", action = "Upload" }
            );
             routes.MapRoute(
                name: "Admin",
                url: "admin/{*catchall}",
                defaults: new { controller = "Home", action = "Admin" }
            );
             routes.MapRoute(
                name: "Reports",
                url: "reports/{*catchall}",
                defaults: new { controller = "Home", action = "Reports" }
            );
             routes.MapRoute(
                name: "Reference",
                url: "reference/{*catchall}",
                defaults: new { controller = "Home", action = "Reference" }
            );
             routes.MapRoute(
                name: "Utilities",
                url: "utilities/{*catchall}",
                defaults: new { controller = "Home", action = "Utilities" }
            );
             routes.MapRoute(
               name: "Enterprise",
               url: "enterprise/{*catchall}",
               defaults: new { controller = "Home", action = "Enterprise" }
           );
             routes.MapRoute(
               name: "Help",
               url: "help/{*catchall}",
               defaults: new { controller = "Home", action = "Help" } 
           );
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Login", action = "Index" }
                );
            routes.MapRoute(
                name: "Login",
                url: "{controller}/{action}",
                defaults: new { controller = "Login", action = "Index"}
                );
            routes.MapRoute(
            name: "Cart",
            url: "{controller}/{action}",
            defaults: new { controller = "Home", action = "addtocart" }
            );


            
        }
    }
}
