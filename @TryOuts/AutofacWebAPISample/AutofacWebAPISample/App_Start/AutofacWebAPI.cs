using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;
using Autofac;
using Autofac.Integration.WebApi;
using AutofacWebAPISample.Application;

namespace AutofacWebAPISample {

    internal class AutofacWebAPI {

        public static void Initialize(HttpConfiguration config) {

            config.DependencyResolver = new AutofacWebApiDependencyResolver(
                RegisterServices(new ContainerBuilder())
            );
        }

        private static IContainer RegisterServices(ContainerBuilder builder) {

            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly()).PropertiesAutowired();

            //deal with your dependencies here
            builder.RegisterType<CarsService>().As<ICarsService>();

            return builder.Build();
        }
    }
}