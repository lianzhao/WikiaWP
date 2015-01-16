using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml.Linq;

using LianZhao;
using LianZhao.Linq;

namespace MediaWiki.Parse
{
    public class ParseApiClient : DisposableObjectOwner
    {
        private readonly string _site;

        private readonly HttpClient _httpClient;

        public ParseApiClient(string site)
            : this(site, new HttpClient(), isOwner: true)
        {
        }

        public ParseApiClient(string site, HttpClient httpClient, bool isOwner = false)
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

        public async Task<string> ParseAsync(string pageTitle, string property)
        {
            var uri = string.Format("{0}/api.php?action=parse&page={1}&prop={2}&format=xml", _site, pageTitle, property);
            try
            {
                var xml = await _httpClient.GetStringAsync(uri);
                var root = XElement.Parse(xml);
                var element =
                    root.Descendants()
                        .FirstOrDefault(e => e.Name.LocalName.Equals(property, StringComparison.OrdinalIgnoreCase));
                return element == null ? null : element.Value;
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

        public async Task<IDictionary<string, string>> ParseAsync(string pageTitle, IEnumerable<string> properties)
        {
            var uri = string.Format(
                "{0}/api.php?action=parse&page={1}&prop={2}&format=xml",
                _site,
                pageTitle,
                properties.JoinToString("|"));
            try
            {
                var xml = await _httpClient.GetStringAsync(uri);
                var root = XElement.Parse(xml);
                return
                    root.Descendants()
                        .Where(e => properties.Contains(e.Name.LocalName, StringComparer.OrdinalIgnoreCase))
                        .ToDictionary(e => e.Name.LocalName, e => e.Value);
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