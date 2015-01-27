using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

using LianZhao;

using Newtonsoft.Json;

namespace Wikia.SearchSuggestions
{
    public class SearchSuggestionsApiClient : DisposableObjectOwner
    {
        private readonly string _site;

        private readonly HttpClient _httpClient;

        public SearchSuggestionsApiClient(string site)
            : this(site, new HttpClient(), isOwner: true)
        {
        }

        public SearchSuggestionsApiClient(string site, HttpClient httpClient, bool isOwner = false)
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

        public async Task<IEnumerable<string>> GetSearchSuggestionAsync(string query)
        {
            if (query == null)
            {
                throw new ArgumentNullException("query");
            }
            if (string.IsNullOrWhiteSpace(query))
            {
                throw new ArgumentException("Query string can not be empty.", "query");
            }

            var uri = string.Format("{0}/api/v1/SearchSuggestions/List?query={1}", _site, query);
            try
            {
                var json = await _httpClient.GetStringAsync(uri);
                var result = JsonConvert.DeserializeObject<SearchSuggestionsResultSet>(json);
                return result.items.Select(item => item.title);
            }
            catch (Exception ex)
            {
                if (Debugger.IsAttached)
                {
                    Debugger.Break();
                }
                throw;
            }
        }
    }
}