using System.Web.Mvc;

using ZhAsoiafWiki.Plus.Web.Models;

namespace ZhAsoiafWiki.Plus.Web.Controllers
{
    public class ArticlesController : Controller
    {
        public ActionResult Index()
        {
            return View(new ArticlesIndexViewModel { Articles = AppCache.Articles });
        }
    }
}