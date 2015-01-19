using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using LianZhao;
using LianZhao.Linq;

using Newtonsoft.Json;

namespace MediaWiki.Query.CategoryMembers
{
    public class CategoryMembersApiClient : DisposableObjectOwner
    {
        private readonly string _site;

        private readonly HttpClient _httpClient;

        public CategoryMembersApiClient(string site)
            : this(site, new HttpClient(), isOwner: true)
        {
        }

        public CategoryMembersApiClient(string site, HttpClient httpClient, bool isOwner = false)
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

        public async Task<CategoryMemberResultSet> GetCategoryMembersAsync(string title, IEnumerable<CatergoryMemberType> types = null, int count = 0)
        {
            var builder =
                new StringBuilder(_site).Append("/api.php?action=query&list=categorymembers&cmprop=ids|title|type&format=json&cmtitle=")
                    .Append(title);
            if (types != null)
            {
                builder.Append("&cmtype=").Append(types.JoinToString("|"));
            }
            if (count > 0)
            {
                builder.Append("&cmlimit=").Append(count);
            }

            var uri = builder.ToString();
            try
            {
                var json = await _httpClient.GetStringAsync(uri);
                return JsonConvert.DeserializeObject<CategoryMemberResultSet>(json);
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