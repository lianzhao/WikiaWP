using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using LianZhao;

using Newtonsoft.Json;

namespace MediaWiki.Query.AllPages
{
    public class AllPagesApiClient : DisposableObjectOwner
    {
        private const int MaxCount = 500;

        private readonly string _site;

        private readonly HttpClient _httpClient;

        public AllPagesApiClient(string site)
            : this(site, new HttpClient(), isOwner: true)
        {
        }

        public AllPagesApiClient(string site, HttpClient httpClient, bool isOwner = false)
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

        public async Task<IEnumerable<Page>> GetAllCategoriesAsync(
            string titlePrefix = null,
            int @namespace = 0,
            apfilterredir redirect = apfilterredir.all,
            apfilterlanglinks langlinks = apfilterlanglinks.all,
            SortDirection sortDirection = SortDirection.ascending)
        {
            string from = null;
            var rv = Enumerable.Empty<Page>();
            while (true)
            {
                var result =
                    await
                    GetPagesAsync(
                        from,
                        null,
                        titlePrefix,
                        @namespace,
                        redirect,
                        langlinks,
                        sortDirection,
                        count: MaxCount);
                rv = rv.Concat(result.query.pages.Values);
                from = result.querycontinue == null || result.querycontinue.allpages == null
                           ? null
                           : result.querycontinue.allpages.gapfrom;
                if (string.IsNullOrEmpty(from))
                {
                    break;
                }
            }

            return rv;
        }

        public async Task<AllPagesResultSet> GetPagesAsync(
            string from = null,
            string to = null,
            string titlePrefix = null,
            int @namespace = 0,
            apfilterredir redirect = apfilterredir.all,
            apfilterlanglinks langlinks = apfilterlanglinks.all,
            SortDirection sortDirection = SortDirection.ascending,
            int count = 0)
        {
            var builder =
                new StringBuilder(_site).Append("/api.php?action=query&generator=allpages&prop=pageprops&format=json");

            if (!string.IsNullOrEmpty(from))
            {
                builder.Append("&gapfrom=").Append(from);
            }
            if (!string.IsNullOrEmpty(to))
            {
                builder.Append("&gapto=").Append(to);
            }
            if (!string.IsNullOrEmpty(titlePrefix))
            {
                builder.Append("&gapprefix=").Append(titlePrefix);
            }
            if (count > 0)
            {
                builder.Append("&gaplimit=").Append(count);
            }
            builder.Append("&gapnamespace=").Append(@namespace);
            builder.Append("&gapfilterredir=").Append(redirect.ToString());
            builder.Append("&gapfilterlanglinks=").Append(langlinks.ToString());
            builder.Append("&gapdir=").Append(sortDirection.ToString());

            var uri = builder.ToString();
            try
            {
                var json = await _httpClient.GetStringAsync(uri);
                return JsonConvert.DeserializeObject<AllPagesResultSet>(json);
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