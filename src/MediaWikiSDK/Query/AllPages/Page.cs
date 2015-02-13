namespace MediaWiki.Query.AllPages
{
    public class Page
    {
        public int pageid { get; set; }
        public int ns { get; set; }
        public string title { get; set; }
        public Pageprops pageprops { get; set; }
    }
}