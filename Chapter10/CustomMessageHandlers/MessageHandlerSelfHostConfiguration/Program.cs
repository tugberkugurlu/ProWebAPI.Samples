﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.SelfHost;
using System.Web.Http.Routing;
using MessageHandlerSelfHostConfiguration.MessageHandlers;

namespace MessageHandlerSelfHostConfiguration
{
    class Program
    {
        static void Main(string[] args)
        {
            var config = new HttpSelfHostConfiguration(new Uri("http://localhost:5478"));

            config.MessageHandlers.Add(new CustomMessageHandler1());
            config.MessageHandlers.Add(new CustomMessageHandler2());

            //Lines omitted for brevity
            
            
            config.Routes.MapHttpRoute(
                "DefaultHttpRoute",
                "api/{controller}/{id}",
                new { id = RouteParameter.Optional }
            );

            using (var server = new HttpSelfHostServer(config))
            {
                server.OpenAsync().Wait();
                Console.ReadLine();
            } 
        }
    }
}
