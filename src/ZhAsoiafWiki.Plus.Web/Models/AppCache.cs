using System.Collections.Generic;
using System.Runtime.Caching;

namespace ZhAsoiafWiki.Plus.Web.Models
{
    public static class AppCache
    {
        public static readonly string CacheStatusKey = typeof(CacheStatus).FullName;

        public static CacheStatus GetStatus(this ObjectCache cache)
        {
            return cache[CacheStatusKey] as CacheStatus;
        }

        public static void SetStatus(this ObjectCache cache, CacheStatus status)
        {
            cache[CacheStatusKey] = status;
        }

        public static IDictionary<string, string> GetCachedDictionary(this ObjectCache cache, CacheModule module)
        {
            var key = module.ToString();
            return cache[key] as IDictionary<string, string>;
        }

        public static void SetCacheModule(this ObjectCache cache, CacheModule module, object value)
        {
            var key = module.ToString();
            cache[key] = value;
        }
    }
}