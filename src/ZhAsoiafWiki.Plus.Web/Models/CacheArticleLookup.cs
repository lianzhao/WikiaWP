using System;
using System.Runtime.Caching;
using System.Threading;
using System.Threading.Tasks;

using LianZhao.Patterns.Func;

using ZhAsoiafWiki.Plus.Models;

namespace ZhAsoiafWiki.Plus.Web.Models
{
    public class CacheArticleLookup : IAsyncFunc<string, Article>
    {
        private static readonly TimeSpan CacheTimeout = TimeSpan.FromHours(1);

        private readonly ObjectCache _cache;

        private readonly IAsyncFunc<string, Article> _fallbackLookup;

        public CacheArticleLookup(ObjectCache cache = null, IAsyncFunc<string, Article> fallbackLookup = null)
        {
            _cache = cache ?? MemoryCache.Default;
            _fallbackLookup = fallbackLookup;
        }

        public async Task<Article> InvokeAsync(
            string title,
            CancellationToken cancellationToken = new CancellationToken())
        {
            var cacheKey = GetCacheKey(title);
            var article = _cache[cacheKey] as Article;
            if (article == null)
            {
                if (_fallbackLookup == null)
                {
                    return null;
                }
                article = await _fallbackLookup.InvokeAsync(title, cancellationToken);
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

        private static string GetCacheKey(string title)
        {
            return string.Format("{0}{1}", typeof(Article).FullName, title);
        }
    }
}