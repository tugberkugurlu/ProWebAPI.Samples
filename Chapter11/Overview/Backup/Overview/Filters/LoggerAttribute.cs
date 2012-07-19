using System;
using System.Web.Http.Filters;
using System.Threading.Tasks;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Threading;
using System.Diagnostics;

namespace Overview.Filters {

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class LoggerAttribute : Attribute, IActionFilter {

        public Task<HttpResponseMessage> ExecuteActionFilterAsync(
            HttpActionContext actionContext, 
            CancellationToken cancellationToken, 
            Func<Task<HttpResponseMessage>> continuation) {

            Trace.TraceInformation(
                string.Format(
                    "Controller {0}, Action {1} is running...", 
                    actionContext.ControllerContext.ControllerDescriptor.ControllerName, 
                    actionContext.ActionDescriptor.ActionName
                )
            );

            //the way of saying everything is OK
            return continuation();
        }

        public bool AllowMultiple {
            get { 
                return false;
            }
        }
    }
}