using System;
using System.Collections.Generic;
using System.IO;
using System.IO.IsolatedStorage;
using System.Net.Http;
using System.Threading.Tasks;

using LianZhao;

using Newtonsoft.Json;

namespace WikiaWP
{
    public class ApiClient : DisposableObjectOwner
    {
        private const string Site = "http://zh.asoiaf.wikia.com";

        private readonly HttpClient _httpClient;

        private readonly Lazy<Wikia.ApiClient> _wikiaLazy;

        private readonly Lazy<Wikia.Asoiaf.Zh.ApiClient> _zhAsoiafWikiLazy;

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

        public static async Task RefreshCacheAsync()
        {
            using (var api = new ApiClient())
            {
                var tasks = new[]
                            {
                                api.ZhAsoiafWiki.Dictionaries.GetMainDictAsync(),
                                api.ZhAsoiafWiki.Dictionaries.GetRedirectDictAsync()
                            };
                var dicts = await Task.WhenAll(tasks);
                MainDictionary = dicts[0];
                RedirectDictionary = dicts[1];
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