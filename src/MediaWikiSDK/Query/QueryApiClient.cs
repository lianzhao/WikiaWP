using System;
using System.Net.Http;

using LianZhao;

using MediaWiki.Query.AllCategories;
using MediaWiki.Query.AllPages;
using MediaWiki.Query.CategoryMembers;

namespace MediaWiki.Query
{
    public class QueryApiClient : DisposableObjectOwner
    {
        private readonly string _site;

        private readonly HttpClient _httpClient;

        private readonly Lazy<CategoryMembersApiClient> _cmLazy;

        private readonly Lazy<AllCategoriesApiClient> _acLazy;

        private readonly Lazy<AllPagesApiClient> _apLazy;

        public QueryApiClient(string site)
            : this(site, new HttpClient(), isOwner: true)
        {
        }

        public QueryApiClient(string site, HttpClient httpClient, bool isOwner = false)
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
            _cmLazy = new Lazy<CategoryMembersApiClient>(() => new CategoryMembersApiClient(_site, _httpClient, isOwner: false));
            _acLazy = new Lazy<AllCategoriesApiClient>(() => new AllCategoriesApiClient(_site, _httpClient, isOwner: false));
            _apLazy = new Lazy<AllPagesApiClient>(() => new AllPagesApiClient(_site, _httpClient, isOwner: false));
        }

        public CategoryMembersApiClient CategoryMembers
        {
            get
            {
                return _cmLazy.Value;
            }
        }

        public AllCategoriesApiClient AllCategories
        {
            get
            {
                return _acLazy.Value;
            }
        }

        public AllPagesApiClient AllPages
        {
            get
            {
                return _apLazy.Value;
            }
        }
    }
}