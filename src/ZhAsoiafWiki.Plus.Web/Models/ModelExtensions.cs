using Wikia.Articles;

using ZhAsoiafWiki.Plus.Models;

namespace ZhAsoiafWiki.Plus.Web.Models
{
    public static class ModelExtensions
    {
        //public static string ToPinYin(string input)
        //{
        //    var sb = new StringBuilder()
        //    foreach (var c in input)
        //    {
        //        var chChar = new ChineseChar(c);
        //        chChar.Pinyins[0]
        //    }
        //}

        //public static SimpleArticle ToPlusSimpleArticleModel(this ExpandedArticle article)
        //{
        //    if (article == null)
        //    {
        //        return null;
        //    }
        //    return new SimpleArticle
        //    {
        //        Id = article.id,
        //        Namespace = article.ns,
        //        Title = article.title
        //    };
        //}

        public static Article ToPlusArticleModel(this ExpandedArticle article)
        {
            if (article == null)
            {
                return null;
            }
            var rv = new Article
            {
                Id = article.id,
                Namespace = article.ns,
                Title = article.title,
                Uri = article.url,
                Abstract = article.@abstract,
                ImageUri = article.GetOriginalImageUri(),
            };
            if (article.original_dimensions != null)
            {
                rv.ImageWidth = article.original_dimensions.width;
                rv.ImageHeight = article.original_dimensions.height;
            }
            return rv;
        }
    }
}