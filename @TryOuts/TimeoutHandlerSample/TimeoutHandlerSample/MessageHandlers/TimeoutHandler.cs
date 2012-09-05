using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace TimeoutHandlerSample.MessageHandlers {

    public class TimeoutHandler : DelegatingHandler {

        protected async override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken) {

            var cts = new CancellationTokenSource(2000);
            var linkedToken = CancellationTokenSource.CreateLinkedTokenSource(cts.Token, cancellationToken).Token;

            try {

                return await base.SendAsync(request, linkedToken);
            }
            catch (TaskCanceledException ex) {

                return request.CreateResponse(HttpStatusCode.RequestTimeout, ex);
            }
        }
    }
}