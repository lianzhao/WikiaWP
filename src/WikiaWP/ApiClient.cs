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

        public static ApiClient Instance { get; set; }

        private readonly HttpClient _httpClient;

        private readonly Lazy<Wikia.ApiClient> _wikiaLazy;

        private readonly Lazy<Wikia.Asoiaf.Zh.ApiClient> _zhAsoiafWikiLazy;

        public IDictionary<string, string> MainDictionary { get; private set; }

        public IDictionary<string,string> RedirectDictionary { get; private set; }

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

        public Wikia.ApiClient WikiaApi
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

        public async Task RefreshCacheAsync()
        {
            var tasks = new[] { RefreshMainDictionaryAsync(), RefreshRedirectDictionary() };
            await Task.WhenAll(tasks);
        }

        private async Task RefreshMainDictionaryAsync()
        {
            var dict = await ZhAsoiafWiki.Dictionaries.GetMainDictAsync();
            MainDictionary = new Dictionary<string, string>(dict, StringComparer.OrdinalIgnoreCase);
        }

        private async Task RefreshRedirectDictionary()
        {
            var dict = await ZhAsoiafWiki.Dictionaries.GetRedirectDictAsync();
            RedirectDictionary = new Dictionary<string, string>(dict, StringComparer.OrdinalIgnoreCase);
        }
    }
}