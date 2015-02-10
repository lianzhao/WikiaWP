using System.Collections.Generic;
using System.Runtime.Caching;

namespace ZhAsoiafWiki.Plus.Web.Models
{
    public static class AppCache
    {
        public const string EnZhDictionaryKey = "EnZhDictionary";

        public const string RedirectDictionaryKey = "RedirectDictionary";

        public static readonly string CacheStatusKey = typeof(CacheStatus).FullName;

        public static CacheStatus GetStatus(this ObjectCache cache)
        {
            return cache[CacheStatusKey] as CacheStatus;
        }

        public static void SetStatus(this ObjectCache cache, CacheStatus status)
        {
            cache[CacheStatusKey] = status;
        }

        public static IDictionary<string, string> GetEnZhDictionary(this ObjectCache cache)
        {
            return cache[EnZhDictionaryKey] as IDictionary<string, string>;
        }

        public static void SetEnZhDictionary(this ObjectCache cache, IDictionary<string, string> dictionary)
        {
            cache[EnZhDictionaryKey] = dictionary;
        }

        public static IDictionary<string, string> GetRedirectDictionary(this ObjectCache cache)
        {
            return cache[RedirectDictionaryKey] as IDictionary<string, string>;
        }

        public static void SetRedirectDictionary(this ObjectCache cache, IDictionary<string, string> dictionary)
        {
            cache[RedirectDictionaryKey] = dictionary;
        }
    }
}