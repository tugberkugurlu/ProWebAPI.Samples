using System.Web.Http.ModelBinding;
using WebApiConneg.ModelBinding;

namespace WebApiConneg.Entities
{
    [ModelBinder(typeof(SearchModelBinderProvider))]
    public class Search {
        public string Text { get; set; }
        public int MaxResults { get; set; }
    }
}