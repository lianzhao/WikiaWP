using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Threading.Tasks;

using MediaWiki.Query.AllPages;

using ZhAsoiafWiki.Plus.Models;

namespace ZhAsoiafWiki.Plus.Web.Models
{
    public static class AppCache
    {
        public static readonly string CacheStatusKey = typeof(CacheStatus).FullName;

        public static IReadOnlyCollection<SimpleArticle> Articles { get; private set; }

        static AppCache()
        {
            Articles = Enumerable.Empty<SimpleArticle>().ToList();
        }

        public static async Task RefreshArticlesAsync(ApiClient api)
        {
            var pages =
                await
                api.MediaWiki.Query.AllPages.GetAllPagesAsync(
                    prop: approp.info | approp.pageprops | approp.links | approp.langlinks);
            Articles = pages.Select(
                p =>
                    {
                        var rv = new SimpleArticle
                                     {
                                         Id = p.pageid,
                                         Namespace = p.ns,
                                         Title = p.title,
                                         PinYin = p.pageprops == null ? null : p.pageprops.defaultsort,
                                         RedirectTo =
                                             (p.redirect != null && p.links != null && p.links.Length > 0)
                                                 ? p.links[0].title
                                                 : null,
                                     };
                        var enLink = p.langlinks == null
                                         ? null
                                         : p.langlinks.FirstOrDefault(
                                             ll => ll.lang.Equals("en", StringComparison.InvariantCultureIgnoreCase));
                        if (enLink != null)
                        {
                            rv.EnLink = enLink._;
                        }

                        return rv;
                    }).ToList();
        }

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