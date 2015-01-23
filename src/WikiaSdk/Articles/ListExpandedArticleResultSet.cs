namespace Wikia.Articles
{
    public class ListExpandedArticleResultSet
    {
        public ExpandedArticle[] items { get; set; }
        public string basepath { get; set; }
        public string offset { get; set; }
    }
}