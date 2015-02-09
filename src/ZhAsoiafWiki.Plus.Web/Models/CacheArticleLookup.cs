using System;
using System.Runtime.Caching;
using System.Threading;
using System.Threading.Tasks;

using LianZhao.Patterns.Func;

using ZhAsoiafWiki.Plus.Models;

namespace ZhAsoiafWiki.Plus.Web.Models
{
    public class CacheArticleLookup : IAsyncFunc<ArticleCriteria, Article>
    {
        private static readonly TimeSpan CacheTimeout = TimeSpan.FromHours(1);

        private readonly ObjectCache _cache;

        private readonly IAsyncFunc<ArticleCriteria, Article> _fallbackLookup;

        public CacheArticleLookup(ObjectCache cache = null, IAsyncFunc<ArticleCriteria, Article> fallbackLookup = null)
        {
            _cache = cache ?? MemoryCache.Default;
            _fallbackLookup = fallbackLookup;
        }

        public async Task<Article> InvokeAsync(
            ArticleCriteria criteria,
            CancellationToken cancellationToken = new CancellationToken())
        {
            var cacheKey = GetCacheKey(criteria);
            var article = _cache[cacheKey] as Article;
            if (article == null)
            {
                if (_fallbackLookup == null)
                {
                    return null;
                }
                article = await _fallbackLookup.InvokeAsync(criteria, cancellationToken);
                if (article != null)
                {
                    _cache.Add(cacheKey, article, DateTimeOffset.Now.Add(CacheTimeout));
                }
            }
            else
            {
                _cache.Set(cacheKey, article, DateTimeOffset.Now.Add(CacheTimeout));
            }
            return article;
        }

        private static string GetCacheKey(ArticleCriteria criteria)
        {
            return string.Format("{0}{1}", typeof(Article).FullName, criteria.Title);
        }
    }
}