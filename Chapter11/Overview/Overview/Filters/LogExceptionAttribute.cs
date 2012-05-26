using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Filters;
using System.Threading.Tasks;
using System.Threading;
using System.Diagnostics;

namespace Overview.Filters {

    public class LogExceptionAttribute : Attribute, IExceptionFilter {

        public Task ExecuteExceptionFilterAsync(
            HttpActionExecutedContext actionExecutedContext, 
            CancellationToken cancellationToken) {

            Trace.TraceInformation(
                string.Format(
                    "Exception, related to Controller {0}, Action {1}...",
                    actionExecutedContext.ActionContext.ControllerContext.ControllerDescriptor.ControllerName,
                    actionExecutedContext.ActionContext.ActionDescriptor.ActionName
                )
            );

            return Task.Factory.StartNew(() => { });
        }

        public bool AllowMultiple {
            get { return false; }
        }
    }
}