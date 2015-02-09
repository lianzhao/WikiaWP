using System;
using System.Linq;
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
                using (var api = new ApiClient())
                {
                    var fallback = new WikiaArticleLoopup(api.Wikia);
                    var lookup = new CacheArticleLookup(fallbackLookup: fallback);
                    var resultSet = await api.Wikia.Search.Search(criteria.Query, criteria.Page, criteria.PageSize);
                    result.Articles =
                        await
                        Task.WhenAll(
                            resultSet.items.Select(
                                async item =>
                                await lookup.InvokeAsync(item.title)
                                ?? new Article { Id = item.id, Namespace = item.ns, Title = item.title, Uri = item.url }));
                    result.TotalCount = resultSet.total;
                    result.Page = criteria.Page;
                    result.PageSize = criteria.PageSize;
                }
            }
            return result;
        }
    }
}