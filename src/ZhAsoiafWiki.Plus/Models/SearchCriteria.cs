namespace ZhAsoiafWiki.Plus.Models
{
    public class SearchCriteria
    {
        public string Query { get; set; }

        public int Page { get; set; }

        public int? PageSize { get; set; }

        public int? AbstractLength { get; set; }

        public int? ThumbnailWidth { get; set; }

        public int? ThumbnailHeight { get; set; }

        public bool IsValidRequest()
        {
            return !string.IsNullOrEmpty(Query) && Page >= 0;
        }

        public bool IsExactSearch()
        {
            return Page == 0;
        }

        public ArticleCriteria ToArticleCriteria()
        {
            return new ArticleCriteria
                       {
                           Title = Query,
                           AbstractLength = AbstractLength,
                           ThumbnailWidth = ThumbnailWidth,
                           ThumbnailHeight = ThumbnailHeight
                       };
        }
    }
}