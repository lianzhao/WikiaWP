using System;
using System.Net.Http;

using LianZhao;

using MediaWiki.Parse;

namespace MediaWiki
{
    public class ApiClient : DisposableObjectOwner
    {
        private readonly string _site;

        private readonly HttpClient _httpClient;

        private readonly Lazy<ParseApiClient> _parseLazy;

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
            _parseLazy = new Lazy<ParseApiClient>(() => new ParseApiClient(_site, _httpClient, isOwner: false));
        }

        public ParseApiClient Parse
        {
            get
            {
                return _parseLazy.Value;
            }
        }
    }
}