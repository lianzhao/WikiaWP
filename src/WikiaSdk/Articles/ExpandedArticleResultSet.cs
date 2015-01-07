using System.Collections.Generic;

namespace Wikia.Articles
{
    public class ExpandedArticleResultSet
    {
        public IEnumerable<ExpandedArticle> items { get; set; }

        public string basepath { get; set; }
    }
}