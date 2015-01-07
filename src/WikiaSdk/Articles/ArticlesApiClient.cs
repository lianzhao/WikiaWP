using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using LianZhao.Linq;

using Newtonsoft.Json;

namespace Wikia.Articles
{
    public class ArticlesApiClient
    {
        private readonly HttpClient _httpClient;

        public async Task<ExpandedArticle> GetArticleAsync(int id,
            int @abstract = 0,
            int width = 0,
            int height = 0)
        {
            var articles = await GetArticlesAsync(new[] { id }, @abstract, width, height);
            return articles == null ? null : articles.FirstOrDefault(m => m.id == id);
        }

        public Task<IEnumerable<ExpandedArticle>> GetArticlesAsync(
            IEnumerable<int> ids,
            int @abstract = 0,
            int width = 0,
            int height = 0)
        {
            return GetArticlesAsync(ids, null, @abstract, width, height);
        }

        public async Task<IEnumerable<ExpandedArticle>> GetArticlesAsync(
            IEnumerable<int> ids,
            IEnumerable<string> titles,
            int @abstract = 0,
            int width = 0,
            int height = 0)
        {
            var builder = new StringBuilder("http://zh.asoiaf.wikia.com/api/v1/Articles/Details?");
            var paraIds = ids.JoinToString(",");
            if (!string.IsNullOrEmpty(paraIds))
            {
                builder.Append("ids=").Append(paraIds);
            }
            var paraTitles = titles.JoinToString(",");
            if (!string.IsNullOrEmpty(paraTitles))
            {
                builder.Append("&titles=").Append(paraTitles);
            }
            if (@abstract > 0)
            {
                builder.Append("&abstract=").Append(@abstract);
            }
            if (width > 0)
            {
                builder.Append("&width=").Append(width);
            }
            if (height > 0)
            {
                builder.Append("&abstract=").Append(height);
            }

            var uri = builder.ToString();
            var json = await _httpClient.GetStringAsync(uri);
            return JsonConvert.DeserializeObject<IEnumerable<ExpandedArticle>>(json);
        }
    }
}
