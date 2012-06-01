using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Filters;
using System.Diagnostics;
using System.Web.Http.Controllers;

namespace ActionFiltersAndOrdering.Filters {

    public class SecondaryLoggerAttribute : ActionFilterAttribute {

        private const string _loggerName = "SecondaryLogger";

        public override void OnActionExecuting(HttpActionContext actionContext) {

            LoggerUtils.WriteLog(
                _loggerName,
                "OnActionExecuting",
                actionContext.ControllerContext.ControllerDescriptor.ControllerName,
                actionContext.ActionDescriptor.ActionName
            );
        }

        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext) {

            LoggerUtils.WriteLog(
                _loggerName,
                "OnActionExecuted",
                actionExecutedContext.ActionContext.ControllerContext.ControllerDescriptor.ControllerName,
                actionExecutedContext.ActionContext.ActionDescriptor.ActionName
            );
        }
    }
}