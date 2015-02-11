using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

using LianZhao;
using LianZhao.Patterns.Func;

using Wikia.Articles;

using ZhAsoiafWiki.Plus.Models;

namespace ZhAsoiafWiki.Plus.Web.Models
{
    public class WikiaArticleLoopup : DisposableObjectOwner, IAsyncFunc<string, Article>
    {
        private readonly Wikia.ApiClient _api;

        public WikiaArticleLoopup()
            : this(new Wikia.ApiClient(ApiClient.Site), isOwner: true)
        {
        }

        public WikiaArticleLoopup(Wikia.ApiClient api, bool isOwner = false)
            : base(api, isOwner)
        {
            _api = api;
        }

        public async Task<Article> InvokeAsync(string title, CancellationToken cancellationToken = new CancellationToken())
        {
            try
            {
                var article = await _api.Articles.GetArticleAsync(title, ArticlesApiClient.MaxAbstractLength);
                if (article == null)
                {
                    return null;
                }
                var rv = new Article
                             {
                                 Id = article.id,
                                 Namespace = article.ns,
                                 Title = article.title,
                                 Uri = article.url,
                                 Abstract = article.@abstract,
                                 ImageUri = article.GetOriginalImageUri(),
                             };
                if (article.original_dimensions != null)
                {
                    rv.ImageWidth = article.original_dimensions.width;
                    rv.ImageHeight = article.original_dimensions.height;
                }
                return rv;
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