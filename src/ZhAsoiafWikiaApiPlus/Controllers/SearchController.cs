using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

using ZhAsoiafWikiaApiPlus.Models;

namespace ZhAsoiafWikiaApiPlus.Controllers
{
    public class SearchController : ApiController
    {
        public async Task<Article> Get(string id)
        {
            using (var api = new ApiClient())
            {
                try
                {
                    var article = await api.Wikia.Articles.GetArticleAsync(id);
                    if (article == null)
                    {
                        return null;
                    }
                    return new Article
                               {
                                   Title = article.title,
                                   Uri = article.url,
                                   Abstract = article.@abstract,
                                   ImageThumbnailUri = article.thumbnail,
                                   ImageUri = article.OriginalImageSource,
                               };
                }
                catch (Exception ex)
                {
                    if (Debugger.IsAttached)
                    {
                        Debugger.Break();
                    }
                    throw;
                }

            }
        }
    }
}