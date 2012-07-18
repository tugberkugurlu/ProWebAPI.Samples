using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Controllers;

namespace FirstIHttpController.APIs {

    public class CarsController : IHttpController {

        public Task<HttpResponseMessage> ExecuteAsync(HttpControllerContext controllerContext, CancellationToken cancellationToken) {

            if (controllerContext.Request.Method != HttpMethod.Get) {

                var notAllowedResponse = 
                    new HttpResponseMessage(HttpStatusCode.MethodNotAllowed);

                var notAllowedTcs = new TaskCompletionSource<HttpResponseMessage>();
                notAllowedTcs.SetResult(notAllowedResponse);

                return notAllowedTcs.Task;
            }
            
            var cars = new[] { 
                "Car 1",
                "Car 2",
                "Car 3"
            };

            var response = 
                controllerContext.Request.CreateResponse(HttpStatusCode.OK, cars);

            var tcs = new TaskCompletionSource<HttpResponseMessage>();
            tcs.SetResult(response);

            return tcs.Task;
        }
    }
}