using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace AutofacWebAPISample {

    public class WebApiConfig {

        public static void Configure(HttpConfiguration config) {

            RegisterRoutes(config);
            AutofacWebAPI.Initialize(config);
        }

        public static void RegisterRoutes(HttpConfiguration config) {

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }

    }
}