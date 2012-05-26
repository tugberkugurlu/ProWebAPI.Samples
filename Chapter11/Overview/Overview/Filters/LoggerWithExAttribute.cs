using System;
using System.Web.Http.Filters;
using System.Threading.Tasks;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Threading;
using System.Diagnostics;

namespace Overview.Filters {

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class LoggerWithExAttribute : Attribute, IActionFilter {

        public Task<HttpResponseMessage> ExecuteActionFilterAsync(
            HttpActionContext actionContext, 
            CancellationToken cancellationToken, 
            Func<Task<HttpResponseMessage>> continuation) {

            //assuming something went wrong and threw an exception
            TaskCompletionSource<HttpResponseMessage> tcs = new TaskCompletionSource<HttpResponseMessage>();
            tcs.SetException(new Exception());

            //returning the response with exception
            return tcs.Task;
        }

        public bool AllowMultiple {
            get { 
                return false;
            }
        }
    }
}