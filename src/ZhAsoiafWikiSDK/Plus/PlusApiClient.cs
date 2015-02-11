using System;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using LianZhao;

using Newtonsoft.Json;

using ZhAsoiafWiki.Plus.Models;

namespace ZhAsoiafWiki.Plus
{
    public class PlusApiClient : DisposableObjectOwner
    {
        public const string Site = "http://zhasoiafwikiplus.apphb.com";

        private readonly HttpClient _httpClient;

        public PlusApiClient()
            : this(new HttpClient(), isOwner: true)
        {
        }

        public PlusApiClient(HttpClient httpClient, bool isOwner = false)
            : base(httpClient, isOwner)
        {
            if (httpClient == null)
            {
                throw new ArgumentNullException("httpClient");
            }
            _httpClient = httpClient;
        }

        public async Task<SearchResult> Search(SearchCriteria criteria)
        {
            if (criteria == null)
            {
                throw new ArgumentNullException("criteria");
            }

            if (!criteria.IsValidRequest())
            {
                throw new ArgumentException("Invalid request", "criteria");
            }

            var builder = new StringBuilder(Site).Append("/api/search?query=").Append(criteria.Query);
            if (criteria.Page > 0)
            {
                builder.Append("&page=").Append(criteria.Page);
            }
            if (criteria.PageSize > 0)
            {
                builder.Append("&pagesize=").Append(criteria.PageSize);
            }
            var uri = builder.ToString();
            try
            {
                var json = await _httpClient.GetWikiResultAsync(uri);
                return JsonConvert.DeserializeObject<SearchResult>(json);
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