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

using Enumerable = System.Linq.Enumerable;

namespace MediaWiki.Query.AllCategories
{
    public class AllCategoriesApiClient : DisposableObjectOwner
    {
        private const int MaxCount = 500;

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

        public async Task<IEnumerable<Category>> GetAllCategoriesAsync(
            string titlePrefix = null,
            SortDirection? sortDirection = null,
            int minCategoryMemberCount = 0,
            int maxCategoryMemberCount = 0)
        {
            string from = null;
            var rv = Enumerable.Empty<Category>();
            while (true)
            {
                var result =
                    await
                    GetCategoriesAsync(
                        from,
                        null,
                        titlePrefix,
                        sortDirection,
                        minCategoryMemberCount,
                        maxCategoryMemberCount,
                        count: MaxCount);
                rv = rv.Concat(result.query.allcategories);
                from = result.querycontinue == null || result.querycontinue.allcategories == null
                           ? null
                           : result.querycontinue.allcategories.acfrom;
                if (string.IsNullOrEmpty(from))
                {
                    break;
                }
            }

            return rv;
        }

        public async Task<AllCategoriesResultSet> GetCategoriesAsync(
            string from = null,
            string to = null,
            string titlePrefix = null,
            SortDirection? sortDirection = null,
            int minCategoryMemberCount = 0,
            int maxCategoryMemberCount = 0,
            int count = 0)
        {
            var builder =
                new StringBuilder(_site).Append(
                    "/api.php?action=query&list=allcategories&acprop=size|hidden&format=json");
            if (!string.IsNullOrEmpty(from))
            {
                builder.Append("&acfrom=").Append(from);
            }
            if (!string.IsNullOrEmpty(to))
            {
                builder.Append("&acto=").Append(to);
            }
            if (!string.IsNullOrEmpty(titlePrefix))
            {
                builder.Append("&acprefix=").Append(titlePrefix);
            }
            if (sortDirection != null)
            {
                builder.Append("&acdir=").Append(sortDirection.Value.ToString());
            }
            if (minCategoryMemberCount > 0)
            {
                builder.Append("&acmin=").Append(minCategoryMemberCount);
            }
            if (maxCategoryMemberCount > 0)
            {
                builder.Append("&acmax=").Append(maxCategoryMemberCount);
            }
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

        public async Task<Category> GetCategoryAsyc(string title)
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
                return result.query.allcategories.FirstOrDefault(c => c.title == title);
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