using System.Runtime.Caching;
using System.Web.Http;

using ZhAsoiafWiki.Plus.Web.Models;

namespace ZhAsoiafWiki.Plus.Web.Controllers
{
    public class CacheController : ApiController
    {
        [HttpGet]
        public CacheStatus Status()
        {
            return MemoryCache.Default.GetStatus();
        }
    }
}