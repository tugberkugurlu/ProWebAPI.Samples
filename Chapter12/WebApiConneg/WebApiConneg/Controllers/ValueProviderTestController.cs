using System.Web.Http;

namespace WebApiConneg.Controllers {
    public class ValueProviderTestController : ApiController
    {
        [HttpGet]
        public object GetStuff(string userAgent, string host, int id)
        {
            // userAgent and host are bound from the Headers. id is bound from the query string. 
            // This just echos back. Do something interesting instead.
            return string.Format(
@"User agent: {0},
host: {1}
id: {2}", userAgent, host, id);
        }
    }
}