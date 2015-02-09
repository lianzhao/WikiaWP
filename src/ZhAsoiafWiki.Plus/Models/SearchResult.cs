using System.Collections.Generic;

namespace ZhAsoiafWiki.Plus.Models
{
    public class SearchResult
    {
        public Article MatchArticle { get; set; }

        public int TotalCount { get; set; }

        public int Page { get; set; }

        public int PageSize { get; set; }

        public IEnumerable<Article> Articles { get; set; }
    }
}