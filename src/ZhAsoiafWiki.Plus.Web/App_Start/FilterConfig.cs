using System.Web;
using System.Web.Mvc;

namespace ZhAsoiafWiki.Plus.Web
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
