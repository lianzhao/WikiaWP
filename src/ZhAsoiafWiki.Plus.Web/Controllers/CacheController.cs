using System.Collections.Generic;
using System.Diagnostics;
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
            using (var api = new ApiClient())
            {
                if (module.HasFlag(CacheModule.EnZhDictionary))
                {
                    stopwatch.Restart();
                    var dict = await api.ZhAsoiafWiki.Dictionaries.GetMainDictAsync();
                    cache.SetEnZhDictionary(dict);
                    stopwatch.Stop();
                    rv.Add(CacheModule.EnZhDictionary.ToString(), stopwatch.ElapsedMilliseconds);
                }
                if (module.HasFlag(CacheModule.RedirectDictionary))
                {
                    stopwatch.Restart();
                    var dict = await api.ZhAsoiafWiki.Dictionaries.GetRedirectDictAsync();
                    cache.SetRedirectDictionary(dict);
                    stopwatch.Stop();
                    rv.Add(CacheModule.RedirectDictionary.ToString(), stopwatch.ElapsedMilliseconds);
                }
            }
            return rv;
        }
    }
}