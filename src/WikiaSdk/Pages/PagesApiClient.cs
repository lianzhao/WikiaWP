using System;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;

using LianZhao;

using Newtonsoft.Json;

namespace Wikia.Pages
{
    public class PagesApiClient : DisposableObjectOwner
    {
        private readonly string _site;

        private readonly HttpClient _httpClient;

        public PagesApiClient(string site)
            : this(site, new HttpClient(), isOwner: true)
        {
        }

        public PagesApiClient(string site, HttpClient httpClient, bool isOwner = false)
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

        public async Task<GetPageResultSet> GetPageAsync(string pageTitle)
        {
            var uri = string.Format("{0}/wikia.php?controller=GameGuides&method=getPage&page={1}", _site, pageTitle);
            try
            {
                var json = await _httpClient.GetStringAsync(uri);
                return JsonConvert.DeserializeObject<GetPageResultSet>(json);
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