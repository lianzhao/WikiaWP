using System.Threading.Tasks;

using ZhAsoiafWiki.Plus.Models;

namespace ZhAsoiafWiki.Plus
{
    public static class PlusApiClientExtensions
    {
        public static Task<SearchResult> Search(this PlusApiClient api, string query)
        {
            return api.Search(new SearchCriteria { Query = query });
        }
    }
}