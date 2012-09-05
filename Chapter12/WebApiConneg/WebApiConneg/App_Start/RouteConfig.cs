using System.Web.Http;
using System.Web.Routing;

namespace WebApiConneg
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            //GlobalConfiguration.Configuration.Routes.MapHttpRoute(
            //    "DefaultApi",
            //    routeTemplate: "api/{controller}.{extension}",
            //    defaults: new {},
            //    constraints: new {extension = "json|xml"}
            //    );

            routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}/{format}",
                defaults: new { id = RouteParameter.Optional, format = RouteParameter.Optional }
            );
        }
    }
}