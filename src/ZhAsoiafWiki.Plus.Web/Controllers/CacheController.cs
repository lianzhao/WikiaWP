using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Web.Http;
using System.Web.Mvc;

using ZhAsoiafWiki.Plus.Web.Models;

namespace ZhAsoiafWiki.Plus.Web.Controllers
{
    public class CacheController : ApiController
    {
        [System.Web.Http.HttpGet]
        public CacheStatus Status()
        {
            return MemoryCache.Default.GetStatus();
        }
    }
}