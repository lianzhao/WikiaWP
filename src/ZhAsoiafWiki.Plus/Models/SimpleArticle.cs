namespace ZhAsoiafWiki.Plus.Models
{
    public class SimpleArticle
    {
        public int Id { get; set; }

        public int Namespace { get; set; }

        public string Title { get; set; }

        public string PinYin { get; set; }

        public string RedirectTo { get; set; }

        public string EnLink { get; set; }
    }
}