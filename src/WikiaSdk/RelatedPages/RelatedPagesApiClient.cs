using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using LianZhao;
using LianZhao.Linq;

using Newtonsoft.Json;

namespace Wikia.RelatedPages
{
    public class RelatedPagesApiClient : DisposableObjectOwner
    {
        private readonly string _site;

        private readonly HttpClient _httpClient;

        public RelatedPagesApiClient(string site)
            : this(site, new HttpClient(), isOwner: true)
        {
        }

        public RelatedPagesApiClient(string site, HttpClient httpClient, bool isOwner = false)
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

        public async Task<IEnumerable<RelatedPage>> GetRelatedPagesAsync(int id, int count = 0)
        {
            var result = await GetRelatedPagesAsync(new int[id], count);
            return result.items.RelatedPages[id];
        }

        public async Task<RelatedPagesResultSet> GetRelatedPagesAsync(IEnumerable<int> ids, int count = 0)
        {
            var builder = new StringBuilder(_site).Append("/api/v1/RelatedPages/List?ids=")
                .Append(ids.JoinToString(","));
            if (count > 0)
            {
                builder.Append("&limit=").Append(count);
            }

            var uri = builder.ToString();
            try
            {
                var json = await _httpClient.GetStringAsync(uri);
                return JsonConvert.DeserializeObject<RelatedPagesResultSet>(json);
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