using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

using LianZhao;
using LianZhao.Linq;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MediaWiki.OpenSearch
{
    public class OpenSearchApiClient : DisposableObjectOwner
    {
        private readonly string _site;

        private readonly HttpClient _httpClient;

        public OpenSearchApiClient(string site)
            : this(site, new HttpClient(), isOwner: true)
        {
        }

        public OpenSearchApiClient(string site, HttpClient httpClient, bool isOwner = false)
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

        public async Task<IEnumerable<string>> GetSearchSuggestionAsync(
            string search,
            int limit = 0,
            IEnumerable<int> namespaces = null)
        {
            if (search == null)
            {
                throw new ArgumentNullException("search");
            }
            if (string.IsNullOrWhiteSpace(search))
            {
                throw new ArgumentException("Search text can not be empty.", "search");
            }

            var builder = new StringBuilder(_site).Append("/api.php?action=opensearch&search=").Append(search);
            if (limit > 0)
            {
                builder.Append("&limit=").Append(limit);
            }
            if (namespaces != null)
            {
                builder.Append("&namespace=").Append(namespaces.JoinToString("|"));
            }

            var uri = builder.ToString();
            try
            {
                var json = await _httpClient.GetStringAsync(uri);
                var objArray = JsonConvert.DeserializeObject<object[]>(json);
                if (objArray.Length > 1)
                {
                    var array = objArray[1] as JArray;
                    if (array != null)
                    {
                        return array.ToObject<string[]>();
                    }
                }
                return null;
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