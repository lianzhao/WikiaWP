using System;
using System.Net.Http;
using System.Threading.Tasks;

using LianZhao;

using Wikia.Articles;
using Wikia.Pages;
using Wikia.Search;

namespace Wikia
{
    public class ApiClient : DisposableObjectOwner
    {
        private readonly string _site;

        private readonly HttpClient _httpClient;

        private readonly Lazy<ArticlesApiClient> _articlesLazy;

        private readonly Lazy<PagesApiClient> _pagesLazy;

        private readonly Lazy<SearchApiClient> _searchLazy;

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
            _pagesLazy = new Lazy<PagesApiClient>(() => new PagesApiClient(_site, _httpClient, isOwner: false));
            _searchLazy = new Lazy<SearchApiClient>(() => new SearchApiClient(_site, _httpClient, isOwner: false));
        }

        public ArticlesApiClient Articles
        {
            get
            {
                return _articlesLazy.Value;
            }
        }

        public PagesApiClient Pages
        {
            get
            {
                return _pagesLazy.Value;
            }
        }

        public SearchApiClient Search
        {
            get
            {
                return _searchLazy.Value;
            }
        }
    }
}