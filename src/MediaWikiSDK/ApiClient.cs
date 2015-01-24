using System;
using System.Net.Http;

using LianZhao;

using MediaWiki.OpenSearch;
using MediaWiki.Parse;
using MediaWiki.Query;

namespace MediaWiki
{
    public class ApiClient : DisposableObjectOwner
    {
        private readonly string _site;

        private readonly HttpClient _httpClient;

        private readonly Lazy<ParseApiClient> _parseLazy;

        private readonly Lazy<QueryApiClient> _queryLazy;

        private readonly Lazy<OpenSearchApiClient> _openSearchLazy;

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
            _queryLazy = new Lazy<QueryApiClient>(() => new QueryApiClient(_site, _httpClient, isOwner: false));
            _openSearchLazy = new Lazy<OpenSearchApiClient>(() => new OpenSearchApiClient(_site, _httpClient, isOwner: false));
        }

        public ParseApiClient Parse
        {
            get
            {
                return _parseLazy.Value;
            }
        }

        public QueryApiClient Query
        {
            get
            {
                return _queryLazy.Value;
            }
        }

        public OpenSearchApiClient OpenSearch
        {
            get
            {
                return _openSearchLazy.Value;
            }
        }
    }
}