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

            if (criteria.IsExactSearch())
            {
                var fallback =
                    new LazyAsyncFunc<string, Article>(
                        new Lazy<IAsyncFunc<string, Article>>(() => new WikiaArticleLoopup()));
                var lookup = new CacheArticleLookup(fallbackLookup: fallback);
                var article = await lookup.InvokeAsync(criteria.Query);
                return article == null ? await SearchAsync(criteria) : new SearchResult { MatchArticle = article };
            }
            else
            {
                return await SearchAsync(criteria);
            }
        }

        private static async Task<SearchResult> SearchAsync(SearchCriteria criteria)
        {
            var result = new SearchResult();
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
                result.Page = resultSet.currentBatch;
                result.PageSize = (resultSet.total / resultSet.batches)
                                  + ((resultSet.total % resultSet.batches) == 0 ? 0 : 1);
            }
            return result;
        }
    }
}