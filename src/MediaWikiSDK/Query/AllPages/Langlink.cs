using Newtonsoft.Json;

namespace MediaWiki.Query.AllPages
{
    public class Langlink
    {
        public string lang { get; set; }
        [JsonProperty("*")]
        public string _ { get; set; }
    }
}