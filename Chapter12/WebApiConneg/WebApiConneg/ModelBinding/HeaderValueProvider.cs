using System.Globalization;
using System.Net.Http.Headers;
using System.Reflection;
using System.Web.Http.ValueProviders;

namespace WebApiConneg.ModelBinding {
    public class HeaderValueProvider : IValueProvider
    {
        readonly HttpRequestHeaders _headers;

        public HeaderValueProvider(HttpRequestHeaders headers)
        {
            _headers = headers;
        }

        // Headers doesn't support property bag lookup interface, so grab it with reflection.
        PropertyInfo GetProp(string name)
        {
            var p = typeof(HttpRequestHeaders).GetProperty(name,
                    BindingFlags.Instance | BindingFlags.Public | BindingFlags.IgnoreCase);
            return p;
        }

        public bool ContainsPrefix(string prefix)
        {
            var p = GetProp(prefix);
            return p != null;
        }

        public ValueProviderResult GetValue(string key)
        {
            var p = GetProp(key);
            if (p != null)
            {
                object value = p.GetValue(_headers, null);
                string s = value.ToString(); // for simplicity, convert to a string
                return new ValueProviderResult(s, s, CultureInfo.InvariantCulture);
            }
            return null; // none
        }
    }
}