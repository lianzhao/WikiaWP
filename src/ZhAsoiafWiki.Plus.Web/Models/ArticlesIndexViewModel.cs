using System.Collections.Generic;

using ZhAsoiafWiki.Plus.Models;

namespace ZhAsoiafWiki.Plus.Web.Models
{
    public class ArticlesIndexViewModel
    {
        public IReadOnlyCollection<SimpleArticle> Articles { get; set; }
    }
}