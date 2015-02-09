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
        public async Task<SearchResult> Get([FromUri]SearchCriteria criteria)
        {
            if (!criteria.IsValidRequest())
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            var result = new SearchResult();
            if (criteria.IsExactSearch())
            {
                var fallback =
                    new LazyAsyncFunc<string, Article>(
                        new Lazy<IAsyncFunc<string, Article>>(() => new WikiaArticleLoopup()));
                var lookup = new CacheArticleLookup(fallbackLookup: fallback);
                result.MatchArticle = await lookup.InvokeAsync(criteria.Query);
            }
            else
            {
                //todo
            }
            return result;
        }
    }
}