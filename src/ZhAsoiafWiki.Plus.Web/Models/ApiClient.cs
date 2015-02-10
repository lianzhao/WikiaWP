using System;
using System.Net.Http;

using LianZhao;
using ZhAsoiafWikiApiClient = ZhAsoiafWiki.ApiClient;

namespace ZhAsoiafWiki.Plus.Web.Models
{
    public class ApiClient : DisposableObjectOwner
    {
        private readonly HttpClient _httpClient;

        private readonly Lazy<Wikia.ApiClient> _wikiaLazy;

        private readonly Lazy<ZhAsoiafWikiApiClient> _zhAsoiafWikiLazy;

        private readonly Lazy<MediaWiki.ApiClient> _mediaWikiLazy;

        public static string Site
        {
            get
            {
                return ZhAsoiafWikiApiClient.Site;
            }
        }

        public ApiClient()
            : this(new HttpClient(), true)
        {
        }

        public ApiClient(HttpClient httpClient, bool isOwner = false)
            : base(httpClient, isOwner)
        {
            if (httpClient == null)
            {
                throw new ArgumentNullException("httpClient");
            }
            _httpClient = httpClient;
            _wikiaLazy = new Lazy<Wikia.ApiClient>(() => new Wikia.ApiClient(Site, _httpClient, isOwner: false));
            _zhAsoiafWikiLazy =
                new Lazy<ZhAsoiafWiki.ApiClient>(() => new ZhAsoiafWiki.ApiClient(_httpClient, isOwner: false));
            _mediaWikiLazy = new Lazy<MediaWiki.ApiClient>(() => new MediaWiki.ApiClient(Site, _httpClient, isOwner: false));
        }

        public Wikia.ApiClient Wikia
        {
            get
            {
                return _wikiaLazy.Value;
            }
        }

        public ZhAsoiafWiki.ApiClient ZhAsoiafWiki
        {
            get
            {
                return _zhAsoiafWikiLazy.Value;
            }
        }

        public MediaWiki.ApiClient MediaWiki
        {
            get
            {
                return _mediaWikiLazy.Value;
            }
        }
    }
}