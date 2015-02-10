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
    }
}