using System;
using System.Net.Http;

using LianZhao;

using Wikia.Articles;

namespace Wikia
{
    public class ApiClient : DisposableObjectOwner
    {
        private readonly string _site;

        private readonly HttpClient _httpClient;

        private readonly Lazy<ArticlesApiClient> _articlesLazy;

        public ApiClient(string site)
            : this(site, new HttpClient(), isOwner: true)
        {
        }

        public ApiClient(string site, HttpClient httpClient, bool isOwner = false)
            : base(httpClient, isOwner)
        {
            if (string.IsNullOrEmpty(site))
            {
                throw new ArgumentNullException("site");
            }
            if (httpClient == null)
            {
                throw new ArgumentNullException("httpClient");
            }
            _site = site;
            _httpClient = httpClient;
            _articlesLazy = new Lazy<ArticlesApiClient>(() => new ArticlesApiClient(_site, _httpClient, isOwner: false));
        }

        public ArticlesApiClient Articles
        {
            get
            {
                return _articlesLazy.Value;
            }
        }
    }
}