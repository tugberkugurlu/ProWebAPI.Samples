using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAPICaching {

    public class EntityTagKey {

        private readonly string _resourceUri;
        private readonly IEnumerable<string> _headerValues;
        private readonly string _toString;
        private readonly string _routePattern;

        private const string EntityTagKeyFormat = "{0}-{1}";
    }
}