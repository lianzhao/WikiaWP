namespace ZhAsoiafWiki.Plus.Models
{
    public class Article
    {
        public int Id { get; set; }

        public int Namespace { get; set; }

        public string Title { get; set; }

        public string Abstract { get; set; }

        public string Uri { get; set; }

        public string ImageUri { get; set; }

        public int ImageWidth { get; set; }

        public int ImageHeight { get; set; }
    }
}