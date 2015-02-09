using System;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;

using LianZhao.Patterns.Func;

using ZhAsoiafWiki.Plus.Models;
using ZhAsoiafWiki.Plus.Web.Models;

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

            var fallback =
                new LazyAsyncFunc<ArticleCriteria, Article>(
                    new Lazy<IAsyncFunc<ArticleCriteria, Article>>(() => new WikiaArticleLoopup()));
            var lookup = new CacheArticleLookup(fallbackLookup: fallback);
            return await lookup.InvokeAsync(criteria.ToArticleCriteria());
        }
    }
}