using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Security;
using System.Web.SessionState;
using Autofac.Integration.WebApi;

namespace AutofacWebAPISample {

    public class Global : System.Web.HttpApplication {

        protected void Application_Start(object sender, EventArgs e) {

            WebApiConfig.Configure(GlobalConfiguration.Configuration);
        }
    }
}