using Newtonsoft.Json;

namespace MediaWiki.Query.AllPages
{
    public class Extlink
    {
        [JsonProperty("*")]
        public string _ { get; set; }
    }
}