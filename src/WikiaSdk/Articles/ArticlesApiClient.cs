﻿using System;
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

namespace Wikia.Articles
{
    public class ArticlesApiClient : DisposableObjectOwner
    {
        private readonly string _site;

        private readonly HttpClient _httpClient;

        public ArticlesApiClient(string site)
            : this(site, new HttpClient(), isOwner: true)
        {
        }

        public ArticlesApiClient(string site, HttpClient httpClient, bool isOwner = false)
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

        public async Task<ExpandedArticle> GetArticleAsync(int id,
            int @abstract = 0,
            int width = 0,
            int height = 0)
        {
            var articles = await GetArticlesAsync(new[] { id }, @abstract, width, height);
            return articles == null ? null : articles.FirstOrDefault(m => m.id == id);
        }

        public async Task<ExpandedArticle> GetArticleAsync(string title,
            int @abstract = 0,
            int width = 0,
            int height = 0)
        {
            var articles = await GetArticlesAsync(new[] { title }, @abstract, width, height);
            return articles == null ? null : articles.FirstOrDefault(m => m.title == title);
        }

        public Task<IEnumerable<ExpandedArticle>> GetArticlesAsync(
            IEnumerable<int> ids,
            int @abstract = 0,
            int width = 0,
            int height = 0)
        {
            return GetArticlesAsync(ids, null, @abstract, width, height);
        }

        public Task<IEnumerable<ExpandedArticle>> GetArticlesAsync(
            IEnumerable<string> titles,
            int @abstract = 0,
            int width = 0,
            int height = 0)
        {
            return GetArticlesAsync(null, titles, @abstract, width, height);
        }

        public async Task<IEnumerable<ExpandedArticle>> GetArticlesAsync(
            IEnumerable<int> ids,
            IEnumerable<string> titles,
            int @abstract = 0,
            int width = 0,
            int height = 0)
        {
            var builder = new StringBuilder(_site).Append("/api/v1/Articles/Details?");
            if (ids != null)
            {
                builder.Append("ids=").Append(ids.JoinToString(","));
            }
            if (titles != null)
            {
                builder.Append("titles=").Append(titles.JoinToString(","));
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
                builder.Append("&height=").Append(height);
            }

            var uri = builder.ToString();
            try
            {
                var json = await _httpClient.GetStringAsync(uri);
                var result = JsonConvert.DeserializeObject<ExpandedArticleResultSet>(json);
                return result.items.Values;
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

        public async Task<IEnumerable<ExpandedArticle>> GetTopArticlesAsync(string category = null, int count = 0)
        {
            var builder = new StringBuilder(_site).Append("/api/v1/Articles/Top?expand=1");
            if (!string.IsNullOrEmpty(category))
            {
                builder.Append("&category=").Append(category);
            }
            if (count > 0)
            {
                builder.Append("&limit=").Append(count);
            }

            var uri = builder.ToString();
            try
            {
                var json = await _httpClient.GetStringAsync(uri);
                var result = JsonConvert.DeserializeObject<ExpandedArticleResultSet>(json);
                return result.items.Values;
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

        public async Task<GetArticleCommentsResultSet> GetArticleCommentsAsync(string pageTitle)
        {
            var uri = string.Format("{0}/wikia.php?controller=MercuryApi&method=getArticleComments&title={1}", _site, pageTitle);
            try
            {
                var json = await _httpClient.GetStringAsync(uri);
                json = json.Replace("\"users\":[]", "\"users\":{}");//hack
                return JsonConvert.DeserializeObject<GetArticleCommentsResultSet>(json);
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
