﻿using System.Collections.Generic;
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

        public static T GetCacheModule<T>(this ObjectCache cache, CacheModule module)
            where T : class
        {
            var key = module.ToString();
            return cache[key] as T;
        }

        public static void SetCacheModule(this ObjectCache cache, CacheModule module, object value)
        {
            var key = module.ToString();
            cache[key] = value;
        }
    }
}