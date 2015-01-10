namespace Wikia.Pages
{
    public class GetPageResultSet
    {
        public RelatedPage[] relatedPages { get; set; }
        public string html { get; set; }
        public int revisionid { get; set; }
    }
}