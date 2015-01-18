using System;
using System.Collections.ObjectModel;
using System.Linq;

using Wikia.Mercury;

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
    }
}