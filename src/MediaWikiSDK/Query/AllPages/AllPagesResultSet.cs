using Newtonsoft.Json;

namespace MediaWiki.Query.AllPages
{
    public class AllPagesResultSet
    {
        [JsonProperty("query-continue")]
        public QueryContinue querycontinue { get; set; }
        public Query query { get; set; }
    }
}