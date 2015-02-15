namespace MediaWiki.Query.AllPages
{
    public class QueryContinue
    {
        public Allpages allpages { get; set; }
        public Categories categories { get; set; }
        public Extlinks extlinks { get; set; }
        public Images images { get; set; }
        public Langlinks langlinks { get; set; }
        public Links links { get; set; }
        public Templates templates { get; set; }
    }
}