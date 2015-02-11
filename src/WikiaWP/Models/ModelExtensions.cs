using System;
using System.Collections.ObjectModel;
using System.Linq;

using Wikia;
using Wikia.Mercury;

using WikiaWP.Resources;

namespace WikiaWP.Models
{
    public static class ModelExtensions
    {
        public static ArticleComment_Model ToArticleComment_Model(this Comment c, GetArticleCommentsResultSet comments)
        {
            var user = comments.payload.users[c.userName];
            var userName = user.url.StartsWith(
                "/wiki/Special:",
                StringComparison.OrdinalIgnoreCase)
                               ? ArticleComment_Model.DefaultUserName
                               : c.userName;
            return new ArticleComment_Model
                       {
                           Content =
                               c.text.Replace("<p>", string.Empty).Replace("</p>", string.Empty),
                           CreateUser = userName,
                           CreateDateTimeUtc = c.CreatedUtc,
                           SubComments =
                               c.comments == null || c.comments.Length == 0
                                   ? new ObservableCollection<ArticleComment_Model>()
                                   : new ObservableCollection<ArticleComment_Model>(
                                         c.comments.Select(
                                             sub => sub.ToArticleComment_Model(comments)).ToList())
                       };
        }

        public static ListItem_Model ToListItemModel(
            this ZhAsoiafWiki.Plus.Models.Article article,
            int imageWidth = 0,
            int imageHeight = 0)
        {
            var model = new ListItem_Model
                            {
                                Title = article.Title,
                                Content = article.Abstract,
                                Link = "", // todo
                                ImageSource =
                                    WikiaImageFormatter.ToImageThumbnailUri(
                                        article.ImageUri,
                                        imageWidth,
                                        imageHeight) ?? AppResources.PlaceholderImageSource
                            };
            return model;
        }
    }
}