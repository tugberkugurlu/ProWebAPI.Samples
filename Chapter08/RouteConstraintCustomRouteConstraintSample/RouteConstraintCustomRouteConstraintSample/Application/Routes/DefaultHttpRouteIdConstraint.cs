using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Routing;

namespace RouteConstraintCustomRouteConstraintSample.Application.Routes {

    public class DefaultHttpRouteIdConstraint : IHttpRouteConstraint {

        public bool Match(HttpRequestMessage request, 
            IHttpRoute route, string parameterName, IDictionary<string, object> values, HttpRouteDirection routeDirection) {

            int idParamValue;
            if (values[parameterName] == RouteParameter.Optional || int.TryParse(values[parameterName].ToString(), out idParamValue))
                return true;

            return false;
        }
    }
}