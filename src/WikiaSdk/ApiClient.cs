using System;
using System.Net.Http;

using LianZhao;

using Wikia.Articles;
using Wikia.CuratedContent;
using Wikia.Mercury;
using Wikia.Pages;
using Wikia.RelatedPages;
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

        private readonly Lazy<CuratedContentApiClient> _curatedContentLazy;

        private readonly Lazy<RelatedPagesApiClient> _relatedPagesLazy;

        private readonly Lazy<MercuryApiClient> _mercuryLazy;

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
            _curatedContentLazy = new Lazy<CuratedContentApiClient>(() => new CuratedContentApiClient(_site, _httpClient, isOwner: false));
            _relatedPagesLazy = new Lazy<RelatedPagesApiClient>(() => new RelatedPagesApiClient(_site, _httpClient, isOwner: false));
            _mercuryLazy = new Lazy<MercuryApiClient>(() => new MercuryApiClient(_site, _httpClient, isOwner: false));
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

        public CuratedContentApiClient CuratedContent
        {
            get
            {
                return _curatedContentLazy.Value;
            }
        }

        public RelatedPagesApiClient RelatedPages
        {
            get
            {
                return _relatedPagesLazy.Value;
            }
        }

        public MercuryApiClient Mercury
        {
            get
            {
                return _mercuryLazy.Value;
            }
        }
    }
}