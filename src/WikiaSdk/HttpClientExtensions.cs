using System.Net.Http;
using System.Threading.Tasks;

namespace Wikia
{
    public static class HttpClientExtensions
    {
        public static async Task<string> GetWikiResultAsync(this HttpClient client, string requestUri)
        {
            var res = await client.GetAsync(requestUri);
            if (!res.IsSuccessStatusCode)
            {
                throw new WikiHttpException(res);
            }

            return await res.Content.ReadAsStringAsync();
        }
    }
}