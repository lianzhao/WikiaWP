using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using LianZhao;
using LianZhao.Linq;

using MediaWiki.Query.CategoryMembers;

using Newtonsoft.Json;

namespace MediaWiki.Query.AllCategories
{
    public class AllCategoriesApiClient : DisposableObjectOwner
    {
        private readonly string _site;

        private readonly HttpClient _httpClient;

        public AllCategoriesApiClient(string site)
            : this(site, new HttpClient(), isOwner: true)
        {
        }

        public AllCategoriesApiClient(string site, HttpClient httpClient, bool isOwner = false)
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

        public async Task<AllCategoriesResultSet> GetCategories(int count = 0)
        {
            var builder =
                new StringBuilder(_site).Append(
                    "/api.php?action=query&list=allcategories&acprop=size|hidden&format=json");
            if (count > 0)
            {
                builder.Append("&aclimit=").Append(count);
            }

            var uri = builder.ToString();
            try
            {
                var json = await _httpClient.GetStringAsync(uri);
                return JsonConvert.DeserializeObject<AllCategoriesResultSet>(json);
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

        public async Task<Allcategory> GetCategoryAsyc(string title)
        {
            var uri =
                string.Format(
                    "{0}/api.php?action=query&list=allcategories&acprop=size|hidden&format=json&acfrom={1}&acto={1}",
                    _site,
                    title);
            try
            {
                var json = await _httpClient.GetStringAsync(uri);
                var result = JsonConvert.DeserializeObject<AllCategoriesResultSet>(json);
                return result.query.allcategories.FirstOrDefault(c => c._ == title);
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