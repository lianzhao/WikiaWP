using Newtonsoft.Json;

namespace MediaWiki.Query.AllCategories
{
    public class Category
    {
        [JsonProperty("*")]
        public string title { get; set; }
        public int size { get; set; }
        public int pages { get; set; }
        public int files { get; set; }
        public int subcats { get; set; }
        public string hidden { get; set; }

        public bool IsHidden
        {
            get
            {
                return hidden != null;
            }
        }
    }
}