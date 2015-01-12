using System;
using System.Net.Http;

using LianZhao;

namespace WikiaWP
{
    public class ApiClient : DisposableObjectOwner
    {
        private const string Site = "http://zh.asoiaf.wikia.com";

        public static ApiClient Instance { get; set; }

        private readonly HttpClient _httpClient;

        private readonly Lazy<Wikia.ApiClient> _wikiaLazy;

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
        }

        public Wikia.ApiClient WikiaApi
        {
            get
            {
                return _wikiaLazy.Value;
            }
        }
    }
}