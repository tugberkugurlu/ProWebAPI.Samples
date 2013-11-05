using System;
using System.Linq;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Http;
using System.Web.Http.ModelBinding;
using System.Web.Http.ValueProviders;
using System.Web.Routing;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using WebApiConneg.Formatters;
using WebApiConneg.Mappings;
using WebApiConneg.ModelBinding;

namespace WebApiConneg {
    public class Global : HttpApplication {
        protected void Application_Start(object sender, EventArgs e) {
            var config = GlobalConfiguration.Configuration;

            // Listing 12-24
            config.Formatters.Add(new PlainTextFormatter());

            // Uncomment to test Listing 12-25 to 12-32 and 12-41 
            // then run default.htm from JsonPClient project
            // config.Formatters.Remove(config.Formatters.JsonFormatter);
            // config.Formatters.Insert(0, new JsonpMediaTypeFormatter());

            // Listing 12-34
            config.Formatters.Add(new CarCsvMediaTypeFormatter());
            config.Formatters.Remove(config.Formatters.FirstOrDefault(f => f.GetType() == typeof(JQueryMvcFormUrlEncodedFormatter)));

            // Listing 12-37
            config.Formatters.JsonFormatter.SerializerSettings =
                new JsonSerializerSettings() {
                    ContractResolver = new CamelCasePropertyNamesContractResolver()
                };

             //Uncomment to test Listing 12-45
             config.Formatters.JsonFormatter.MediaTypeMappings.Add(
                new RequestHeaderMapping("Referer", "http://localhost:1501/", StringComparison.InvariantCultureIgnoreCase, false, "text/xml"));

            // Uncomment to test Listing 12-48 to Listing 12-49
            //GlobalConfiguration.Configuration.Formatters.JsonFormatter.
            //    MediaTypeMappings.Add(new RouteDataMapping("extension", "json", new MediaTypeHeaderValue("application/json")));

            //GlobalConfiguration.Configuration.Formatters.XmlFormatter.
            //    MediaTypeMappings.Add(new RouteDataMapping("extension", "xml", new MediaTypeHeaderValue("application/xml")));


            config.Services.Add(
                typeof(ValueProviderFactory), 
                new XHeaderValueProviderFactory());

            config.Services.Add(typeof(ValueProviderFactory), new HeaderValueProviderFactory());
            
            RouteConfig.RegisterRoutes(RouteTable.Routes);


        }
    }
}