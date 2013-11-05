using System.Net.Http.Headers;
using System.Web.Http.Controllers;
using System.Web.Http.ValueProviders;

namespace WebApiConneg.ModelBinding {
    public class HeaderValueProviderFactory : ValueProviderFactory {
        public override IValueProvider GetValueProvider(HttpActionContext actionContext) {
            HttpRequestHeaders headers = actionContext.ControllerContext.Request.Headers;
            return new HeaderValueProvider(headers);
        }
    }
}