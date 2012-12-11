using System;
using System.Globalization;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Threading;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.SelfHost;
using System.Web.Http.ValueProviders;

namespace ConsoleApplication1 {
    internal class Program {
        private static void Main(string[] args) {
            string prefix = "http://localhost:8080";
            HttpSelfHostConfiguration config = new HttpSelfHostConfiguration(prefix);
            config.Routes.MapHttpRoute("Default", "{controller}/{action}");

            // Append our custom valueprovider to the list of value providers.
            config.Services.Add(typeof (ValueProviderFactory), new HeaderValueProviderFactory());

            HttpSelfHostServer server = new HttpSelfHostServer(config);
            server.OpenAsync().Wait();

            try {
                // HttpClient will make the call, but won't set the headers for you. 
                HttpClient client = new HttpClient();
                var response = client.GetAsync(prefix + "/ValueProviderTest/GetStuff?id=20").Result;

                // Browsers will set the headers. 
                // Loop. You can hit the request via: http://localhost:8080/Test2/GetStuff?id=40
                while (true) {
                    Thread.Sleep(1000);
                    Console.Write(".");
                }
            }
            finally {
                server.CloseAsync().Wait();
            }
        }
    }

    public class HeaderValueProvider : IValueProvider {
        private readonly HttpRequestHeaders _headers;

        public HeaderValueProvider(HttpRequestHeaders headers) {
            _headers = headers;
        }

        // Headers doesn't support property bag lookup interface, so grab it with reflection.
        private PropertyInfo GetProp(string name) {
            var p = typeof (HttpRequestHeaders).GetProperty(name,
                                                            BindingFlags.Instance | BindingFlags.Public |
                                                            BindingFlags.IgnoreCase);
            return p;
        }

        public bool ContainsPrefix(string prefix) {
            var p = GetProp(prefix);
            return p != null;
        }

        public ValueProviderResult GetValue(string key) {
            var p = GetProp(key);
            if (p != null) {
                object value = p.GetValue(_headers, null);
                string s = value.ToString(); // for simplicity, convert to a string
                return new ValueProviderResult(s, s, CultureInfo.InvariantCulture);
            }
            return null; // none
        }
    }

    public class HeaderValueProviderFactory : ValueProviderFactory {
        public override IValueProvider GetValueProvider(HttpActionContext actionContext) {
            HttpRequestHeaders headers = actionContext.ControllerContext.Request.Headers;
            return new HeaderValueProvider(headers);
        }
    }

    public class Test2Controller : ApiController {
        
        public object Get([ValueProvider(typeof(HeaderValueProviderFactory))]string userAgent, [ValueProvider(typeof(HeaderValueProviderFactory))]string host, int id)
        {
            // userAgent and host are bound from the Headers. id is bound from the query string. 
            // This just echos back. Do something interesting instead.
            return string.Format(
                @"User agent: {0},
host: {1}
id: {2}", userAgent, host, id);
        }
    }
}