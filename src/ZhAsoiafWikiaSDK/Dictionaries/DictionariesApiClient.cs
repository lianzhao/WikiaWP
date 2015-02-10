using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

using LianZhao;

using Newtonsoft.Json;

namespace ZhAsoiafWiki.Dictionaries
{
    public class DictionariesApiClient : DisposableObjectOwner
    {
        private readonly HttpClient _httpClient;

        public DictionariesApiClient()
            : this(new HttpClient(), isOwner: true)
        {
        }

        public DictionariesApiClient(HttpClient httpClient, bool isOwner = false)
            : base(httpClient, isOwner)
        {
            if (httpClient == null)
            {
                throw new ArgumentNullException("httpClient");
            }
            _httpClient = httpClient;
        }

        public async Task<IDictionary<string, string>> GetMainDictAsync()
        {
            return await RefreshDictAsync(15606);
        }

        public async Task<IDictionary<string, string>> GetRedirectDictAsync()
        {
            return await RefreshDictAsync(15620);
        }

        private async Task<IDictionary<string, string>> RefreshDictAsync(int pageId)
        {
            try
            {
                var uri = string.Format(
                    "http://zh.asoiaf.wikia.com/api.php?action=parse&pageid={0}&prop=text&format=xml",
                    pageId.ToString(CultureInfo.InvariantCulture));
                var xml = await _httpClient.GetStringAsync(uri);
                var start = xml.IndexOf("{", StringComparison.Ordinal);
                var end = xml.IndexOf("}", StringComparison.Ordinal);
                var json = xml.Substring(start, end - start + 1);
                var decoded = WebUtility.HtmlDecode(json);
                return JsonConvert.DeserializeObject<Dictionary<string, string>>(decoded);
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