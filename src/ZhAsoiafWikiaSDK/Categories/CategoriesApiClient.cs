using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

using LianZhao;

using Newtonsoft.Json;

namespace Wikia.Asoiaf.Zh.Categories
{
    public class CategoriesApiClient : DisposableObjectOwner
    {
        private readonly HttpClient _httpClient;

        public CategoriesApiClient()
            : this(new HttpClient(), isOwner: true)
        {
        }

        public CategoriesApiClient(HttpClient httpClient, bool isOwner = false)
            : base(httpClient, isOwner)
        {
            if (httpClient == null)
            {
                throw new ArgumentNullException("httpClient");
            }
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<string>> GetTopCategoriesAsync()
        {
            using (var api = new MediaWiki.ApiClient(ApiClient.Site, _httpClient, isOwner: false))
            {
                var json = await api.Parse.ParseAsync("App/TopCategories", "wikitext");
                return JsonConvert.DeserializeObject<string[]>(json);
            }
        }
    }
}