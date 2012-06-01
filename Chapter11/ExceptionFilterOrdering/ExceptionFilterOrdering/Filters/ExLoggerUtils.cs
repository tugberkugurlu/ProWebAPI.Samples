using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Diagnostics;

namespace ExceptionFilterOrdering.Filters {

    public class ExLoggerUtils {

        public static void WriteLog(
            string loggerName, string controllerName, string actionName, string exceptionMessage) {

            Trace.TraceInformation(
                string.Format(
                    "{0} exception filter, for Controller {1}, Action {2} is running... Exception Message: {3}",
                    loggerName, actionName, controllerName, exceptionMessage
                )
            );
        }
    }
}