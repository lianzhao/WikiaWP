using System;
using System.Net.Http;

using LianZhao;

namespace MediaWiki.Query.CategoryMembers
{
    public class CategoryMembersApiClient : DisposableObjectOwner
    {
        private readonly string _site;

        private readonly HttpClient _httpClient;

        public CategoryMembersApiClient(string site)
            : this(site, new HttpClient(), isOwner: true)
        {
        }

        public CategoryMembersApiClient(string site, HttpClient httpClient, bool isOwner = false)
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
        }
    }
}