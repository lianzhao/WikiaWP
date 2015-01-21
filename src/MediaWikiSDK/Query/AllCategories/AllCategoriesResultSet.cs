using Newtonsoft.Json;

namespace MediaWiki.Query.AllCategories
{
    public class AllCategoriesResultSet
    {
        public Query query { get; set; }
        [JsonProperty("query-continue")]
        public QueryContinue querycontinue { get; set; }
    }
}