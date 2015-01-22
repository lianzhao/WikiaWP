using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

using LianZhao;

namespace WikiaWP
{
    public class ApiClient : DisposableObjectOwner
    {
        private const string Site = "http://zh.asoiaf.wikia.com";

        private readonly HttpClient _httpClient;

        private readonly Lazy<Wikia.ApiClient> _wikiaLazy;

        private readonly Lazy<Wikia.Asoiaf.Zh.ApiClient> _zhAsoiafWikiLazy;

        private readonly Lazy<MediaWiki.ApiClient> _mediaWikiLazy;

        public static IDictionary<string, string> MainDictionary { get; private set; }

        public static IDictionary<string,string> RedirectDictionary { get; private set; }

        public ApiClient()
            : this(new HttpClient(), true)
        {
        }

        public ApiClient(HttpClient httpClient, bool isOwner = false)
            : base(httpClient, isOwner)
        {
            if (httpClient == null)
            {
                throw new ArgumentNullException("httpClient");
            }
            _httpClient = httpClient;
            _wikiaLazy = new Lazy<Wikia.ApiClient>(() => new Wikia.ApiClient(Site, _httpClient, isOwner: false));
            _zhAsoiafWikiLazy =
                new Lazy<Wikia.Asoiaf.Zh.ApiClient>(() => new Wikia.Asoiaf.Zh.ApiClient(_httpClient, isOwner: false));
            _mediaWikiLazy = new Lazy<MediaWiki.ApiClient>(() => new MediaWiki.ApiClient(Site, _httpClient, isOwner: false));
        }

        public Wikia.ApiClient Wikia
        {
            get
            {
                return _wikiaLazy.Value;
            }
        }

        public Wikia.Asoiaf.Zh.ApiClient ZhAsoiafWiki
        {
            get
            {
                return _zhAsoiafWikiLazy.Value;
            }
        }

        public MediaWiki.ApiClient MediaWiki
        {
            get
            {
                return _mediaWikiLazy.Value;
            }
        }

        public static async Task RefreshCacheAsync()
        {
            using (var api = new ApiClient())
            {
                var task1 = api.ZhAsoiafWiki.Dictionaries.GetMainDictAsync();
                var task2 = api.ZhAsoiafWiki.Dictionaries.GetRedirectDictAsync();
                var tasks = new[] { task1 as Task, task2 };
                await Task.WhenAll(tasks);
                MainDictionary = task1.Result;
                RedirectDictionary = task2.Result;
            }
            //await AppCache.SaveToCachedDictionaryAsync(MainDictionary, @"cache\mainDict.json");
            //await AppCache.SaveToCachedDictionaryAsync(RedirectDictionary, @"cache\redirectDict.json");
        }

        //public async Task LoadCacheFromIsolatedStorage()
        //{
        //    MainDictionary = await AppCache.LoadCachedDictionaryAsync(@"cache\mainDict.json") ?? new Dictionary<string, string>();
        //    RedirectDictionary = await AppCache.LoadCachedDictionaryAsync(@"cache\redirectDict.json")
        //                         ?? new Dictionary<string, string>();
        //}
    }
}