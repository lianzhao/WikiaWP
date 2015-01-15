using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using LianZhao;
using LianZhao.Linq;

using Newtonsoft.Json;

namespace Wikia.CuratedContent
{
    public class CuratedContentApiClient : DisposableObjectOwner
    {
        private readonly string _site;

        private readonly HttpClient _httpClient;

        public CuratedContentApiClient(string site)
            : this(site, new HttpClient(), isOwner: true)
        {
        }

        public CuratedContentApiClient(string site, HttpClient httpClient, bool isOwner = false)
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

        public async Task<CuratedContent> GetCuratedContentAsync()
        {
            var uri = string.Format("{0}/wikia.php?controller=GameGuides&method=getList", _site);
            try
            {
                var json = await _httpClient.GetStringAsync(uri);
                return JsonConvert.DeserializeObject<CuratedContent>(json);
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

        public async Task<CuratedContentSection> GetCuratedContentSection(string tag)
        {
            var uri = string.Format("{0}/wikia.php?controller=GameGuides&method=getList&tags={1}", _site, tag);
            try
            {
                var json = await _httpClient.GetStringAsync(uri);
                return JsonConvert.DeserializeObject<CuratedContentSection>(json);
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
