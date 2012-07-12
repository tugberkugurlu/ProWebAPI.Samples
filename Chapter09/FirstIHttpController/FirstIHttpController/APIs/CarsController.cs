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
                return Task.Factory.StartNew<HttpResponseMessage>(() => {

                    return new HttpResponseMessage(HttpStatusCode.NotFound);
                });
            }
            
            var cars = new[] { 
                "Car 1",
                "Car 2",
                "Car 3"
            };

            return Task.Factory.StartNew<HttpResponseMessage>(() => {

                return controllerContext.Request.CreateResponse(HttpStatusCode.OK, cars);
            });
        }
    }
}