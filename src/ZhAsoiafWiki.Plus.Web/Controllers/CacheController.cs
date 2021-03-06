using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Runtime.Caching;
using System.Threading.Tasks;
using System.Web.Http;

using ZhAsoiafWiki.Plus.Web.Models;

namespace ZhAsoiafWiki.Plus.Web.Controllers
{
    public class CacheController : ApiController
    {
        [HttpGet]
        public CacheStatus Status()
        {
            return MemoryCache.Default.GetStatus();
        }

        [HttpGet]
        public async Task<IDictionary<string, long>> Refresh([FromUri] CacheModule module)
        {
            var cache = MemoryCache.Default;
            var stopwatch = new Stopwatch();
            var rv = new Dictionary<string, long>();
            using (var httpClient = new HttpClient {Timeout = TimeSpan.FromMinutes(10)})
            using (var api = new Models.ApiClient(httpClient))
            {
                if (module.HasFlag(CacheModule.EnZhDictionary))
                {
                    stopwatch.Restart();
                    var dict = await api.ZhAsoiafWiki.Dictionaries.GetMainDictAsync();
                    cache.SetCacheModule(CacheModule.EnZhDictionary, dict);
                    stopwatch.Stop();
                    rv.Add(CacheModule.EnZhDictionary.ToString(), stopwatch.ElapsedMilliseconds);
                }
                if (module.HasFlag(CacheModule.RedirectDictionary))
                {
                    stopwatch.Restart();
                    var dict = await api.ZhAsoiafWiki.Dictionaries.GetRedirectDictAsync();
                    cache.SetCacheModule(CacheModule.RedirectDictionary, dict);
                    stopwatch.Stop();
                    rv.Add(CacheModule.RedirectDictionary.ToString(), stopwatch.ElapsedMilliseconds);
                }
                if (module.HasFlag(CacheModule.Articles))
                {
                    stopwatch.Restart();
                    await AppCache.RefreshArticlesAsync(api);
                    stopwatch.Stop();
                    rv.Add(CacheModule.Articles.ToString(), stopwatch.ElapsedMilliseconds);
                }
            }
            return rv;
        }
    }
}