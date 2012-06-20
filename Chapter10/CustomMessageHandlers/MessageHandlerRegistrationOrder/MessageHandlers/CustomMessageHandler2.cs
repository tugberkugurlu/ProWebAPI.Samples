using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Web;

namespace MessageHandlerRegistrationOrder.MessageHandlers
{
    public class CustomMessageHandler2 : DelegatingHandler
    {
        protected override System.Threading.Tasks.Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, System.Threading.CancellationToken cancellationToken)
        {
            Debug.WriteLine("CustomMessageHandler2 request invoked");
            return base.SendAsync(request, cancellationToken).ContinueWith((task) =>
            {
                Debug.WriteLine("CustomMessageHandler2 response invoked");
                var response = task.Result;
                return response;
            });
        }
    }
}