using System;
using System.Net.Http;

using LianZhao;

using MediaWiki.Query.CategoryMembers;

namespace MediaWiki.Query
{
    public class QueryApiClient : DisposableObjectOwner
    {
        private readonly string _site;

        private readonly HttpClient _httpClient;

        private readonly Lazy<CategoryMembersApiClient> _cmLazy;

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
        }

        public CategoryMembersApiClient CategoryMembers
        {
            get
            {
                return _cmLazy.Value;
            }
        }
    }
}