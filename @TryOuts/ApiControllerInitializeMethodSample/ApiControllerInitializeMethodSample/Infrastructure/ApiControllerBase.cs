using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;

namespace ApiControllerInitializeMethodSample.Infrastructure {

    public abstract class ApiControllerBase : ApiController {

        protected override void Initialize(HttpControllerContext controllerContext) {
            
            base.Initialize(controllerContext);
        }

        public override Task<HttpResponseMessage> ExecuteAsync(HttpControllerContext controllerContext, System.Threading.CancellationToken cancellationToken) {

            return base.ExecuteAsync(controllerContext, cancellationToken).ContinueWith(task => {
                var response = task.Result;
                response.Headers.Add("X-Foo", "Bar");

                return response;
            });
        }
    }
}