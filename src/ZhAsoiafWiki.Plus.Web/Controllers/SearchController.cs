using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.Caching;
using System.Threading.Tasks;
using System.Web.Http;

using LianZhao.Collections.Generic;
using LianZhao.Patterns.Func;

using Wikia.Search;

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

            using (var api = new Models.ApiClient())
            {
                var fallback = new WikiaArticleLoopup(api.Wikia);
                var lookup = new CacheArticleLookup(fallbackLookup: fallback);
                if (criteria.IsExactSearch())
                {
                    var keyword = MapKeyword(criteria.Query);
                    var article = await lookup.InvokeAsync(keyword);
                    return article == null ? await SearchAsync(criteria, api, lookup) : new SearchResult { MatchArticle = article };
                }
                return await SearchAsync(criteria, api, lookup);
            }
        }

        private static async Task<SearchResult> SearchAsync(SearchCriteria criteria, Models.ApiClient api, IAsyncFunc<string, Article> lookup)
        {
            var result = new SearchResult();

            SearchResultSet resultSet;
            try
            {
                resultSet = await api.Wikia.Search.Search(criteria.Query, criteria.Page, criteria.PageSize);
            }
            catch (Wikia.WikiHttpException ex)
            {
                throw new HttpResponseException(ex.Response.StatusCode);
            }
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
            result.PageCount = resultSet.batches;
            return result;
        }

        private static string MapKeyword(string keyword)
        {
            foreach (var dict in GetDictionaries())
            {
                if (dict == null)
                {
                    continue;
                }

                string match;
                dict.TryGetValue(keyword, out match, StringComparison.OrdinalIgnoreCase);
                if (!string.IsNullOrEmpty(match))
                {
                    return match;
                }
            }
            return keyword;
        }

        private static IEnumerable<IDictionary<string, string>> GetDictionaries()
        {
            var cache = MemoryCache.Default;
            yield return cache.GetCacheModule<IDictionary<string, string>>(CacheModule.EnZhDictionary);
            yield return cache.GetCacheModule<IDictionary<string, string>>(CacheModule.RedirectDictionary);
        }
    }
}