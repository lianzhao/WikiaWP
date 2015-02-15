using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;

using ZhAsoiafWiki.Plus.Models;
using ZhAsoiafWiki.Plus.Web.Models;

namespace ZhAsoiafWiki.Plus.Web.Controllers
{
    [Route("api/articles")]
    public class ArticlesApiController : ApiController
    {
        public Task<IReadOnlyCollection<SimpleArticle>> Get()
        {
            return Task.FromResult(AppCache.Articles);
        }
    }
}