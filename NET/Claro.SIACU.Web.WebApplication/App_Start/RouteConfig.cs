using System.Web.Mvc;
using System.Web.Routing;

namespace Claro.SIACU.Web.WebApplication
{
    public static class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: Claro.SIACU.Constants.Default,
                url: "{controller}/{action}/{id}",
                defaults: new { controller = Claro.SIACU.Constants.Home, action = Claro.SIACU.Constants.DialogDefault, id = UrlParameter.Optional },
                namespaces: new string[] { "Claro.SIACU.Web.WebApplication.Controllers" }
            );
        }
    }
}
