using System;
using System.Diagnostics;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;

using ZhAsoiafWiki.Plus.Models;

using ZhAsoiafWikiaApiPlus.Models;

namespace ZhAsoiafWiki.Plus.Web.Controllers
{
    public class SearchController : ApiController
    {
        public async Task<Article> Get([FromUri]SearchCriteria criteria)
        {
            if (!criteria.IsValidRequest())
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            using (var api = new ApiClient())
            {
                try
                {
                    var article = await api.Wikia.Articles.GetArticleAsync(criteria.Query);
                    if (article == null)
                    {
                        return null;
                    }
                    return new Article
                               {
                                   Id = article.id,
                                   Namespace = article.ns,
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