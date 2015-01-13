namespace Wikia.Search
{
    public class Item
    {
        public int id { get; set; }
        public string title { get; set; }
        public string url { get; set; }
        public int ns { get; set; }
        public int? quality { get; set; }
    }
}