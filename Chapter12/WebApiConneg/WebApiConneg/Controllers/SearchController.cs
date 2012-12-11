using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using WebApiConneg.Entities;

namespace WebApiConneg.Controllers {
    public class SearchController : ApiController {
        private readonly string[] _persons =
            new[] { "Bill", "Steve", "Scott", "Glenn", "Daniel" };

        public IEnumerable<string> Get(Search search) {
            return _persons
            .Where(w => w.Contains(search.Text))
            .Take(search.MaxResults);
        }
    }
}