namespace Wikia.Search
{
    public class SearchResultSet
    {
        public int total { get; set; }
        public int batches { get; set; }
        public int currentBatch { get; set; }
        public int next { get; set; }
        public Item[] items { get; set; }
    }
}