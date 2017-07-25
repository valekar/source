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

            //Custom Route config to catch all the reouts corresponding to the individual tabs
            routes.MapRoute(
                name: "NewAccount",
                url: "NewAccount/{*catchall}",
                defaults: new { controller = "Home", action = "NewAccount"}
            );

            routes.MapRoute(
                name: "TopAccount",
                url: "TopAccount/{*catchall}",
                defaults: new { controller = "Home", action = "TopAccount" }
            );
            routes.MapRoute(
                name: "Constituent",
                url: "Constituent/{*catchall}",
                defaults: new { controller = "Home", action = "Constituent" }
            );
            routes.MapRoute(
                name: "Transaction",
                url: "Transaction/{*catchall}",
                defaults: new { controller = "Home", action = "Transaction" }
            );
            routes.MapRoute(
               name: "EnterpriseAccount",
               url: "EnterpriseAccount/{*catchall}",
               defaults: new { controller = "Home", action = "EnterpriseAccount" }
           );
            routes.MapRoute(
                name: "Upload",
                url: "upload/{*catchall}",
                defaults: new { controller = "Home", action = "Upload" }
            );
            //Default Route Config
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Login", action = "Index", id = UrlParameter.Optional }
               // defaults: new { controller = "Home", action = "NewAccount", id = UrlParameter.Optional }
            );
        }
    }
}
