using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;

using LianZhao;

using Newtonsoft.Json;

namespace Wikia.Asoiaf.Zh.Pages
{
    public class PagesApiClient : DisposableObjectOwner
    {
        private readonly HttpClient _httpClient;

        public PagesApiClient()
            : this(new HttpClient(), isOwner: true)
        {
        }

        public PagesApiClient(HttpClient httpClient, bool isOwner = false)
            : base(httpClient, isOwner)
        {
            if (httpClient == null)
            {
                throw new ArgumentNullException("httpClient");
            }
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<PageItem>> GetWikiNews()
        {
            return await GetPageItemsAsync("App/WikiNews");
        }

        public async Task<IEnumerable<PageItem>> GetPageItemsAsync(string pageTitle)
        {
            try
            {
                using (var api = new MediaWiki.ApiClient(ApiClient.Site, _httpClient, isOwner: false))
                {
                    var json = await api.Parse.ParseAsync(pageTitle, "wikitext");
                    return JsonConvert.DeserializeObject<PageItem[]>(json);
                }
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