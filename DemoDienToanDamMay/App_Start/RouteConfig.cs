using System.Web.Mvc;
using System.Web.Routing;

namespace DemoDienToanDamMay
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Home",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
            routes.MapRoute(
               name: "File",
               url: "{controller}/{action}/{id}",
               defaults: new { controller = "File", action = "Index", id = UrlParameter.Optional }
           );
        }
    }
}
