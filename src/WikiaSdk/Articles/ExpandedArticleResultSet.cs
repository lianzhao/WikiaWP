using System.Collections.Generic;

namespace Wikia.Articles
{
    public class ExpandedArticleResultSet
    {
        public IDictionary<int, ExpandedArticle> items { get; set; }

        public string basepath { get; set; }
    }
}