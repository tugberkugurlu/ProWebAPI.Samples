using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace TimeoutHandlerSample.MessageHandlers {

    public class LoggerHandler : DelegatingHandler {

        protected async override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken) {

            Trace.TraceInformation(
                "Request Uri: {1}{0}HTTP Method: {2}{0}CorrelationId: {3}",
                Environment.NewLine,
                request.RequestUri.PathAndQuery,
                request.Method,
                request.GetCorrelationId()
            );

            return await base.SendAsync(request, cancellationToken);
        }
    }
}