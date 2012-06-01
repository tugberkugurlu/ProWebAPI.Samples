using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Diagnostics;

namespace SkippedActionFilters.Filters {

    public class LoggerUtils {

        public static void WriteLog(
            string loggerName, string loggerMethodName, string controllerName, string actionName) {

            Trace.TraceInformation(
                string.Format(
                    "{0}, {1} method for Controller {2}, Action {3} is running...",
                    loggerName, loggerMethodName, actionName, controllerName
                )
            );
        }
    }
}