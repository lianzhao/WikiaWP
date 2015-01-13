using System;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using LianZhao;

using Newtonsoft.Json;

namespace Wikia.Search
{
    public class SearchApiClient : DisposableObjectOwner
    {
        private readonly string _site;

        private readonly HttpClient _httpClient;

        public SearchApiClient(string site)
            : this(site, new HttpClient(), isOwner: true)
        {
        }

        public SearchApiClient(string site, HttpClient httpClient, bool isOwner = false)
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

        public async Task<SearchResultSet> Search(string query, int page = 0, int pageSize = 0)
        {
            if (string.IsNullOrEmpty(query))
            {
                throw new ArgumentNullException("query");
            }

            var sb = new StringBuilder(_site).Append("/api/v1/Search/List?query=").Append(query);
            if (page > 0)
            {
                sb.Append("batch=").Append(page);
            }
            if (pageSize > 0)
            {
                sb.Append("limit=").Append(pageSize);
            }
            var uri = sb.ToString();
            try
            {
                var json = await _httpClient.GetStringAsync(uri);
                return JsonConvert.DeserializeObject<SearchResultSet>(json);
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