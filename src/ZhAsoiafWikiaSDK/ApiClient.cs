using System;
using System.Net.Http;

using LianZhao;

using Wikia.Asoiaf.Zh.Dictionaries;

namespace Wikia.Asoiaf.Zh
{
    public class ApiClient : DisposableObjectOwner
    {
        private readonly HttpClient _httpClient;

        private readonly Lazy<DictionariesApiClient> _dictionariesLazy;

        public ApiClient()
            : this(new HttpClient(), isOwner: true)
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
            _dictionariesLazy = new Lazy<DictionariesApiClient>(() => new DictionariesApiClient(_httpClient, isOwner: false));
        }

        public DictionariesApiClient Dictionaries
        {
            get
            {
                return _dictionariesLazy.Value;
            }
        }
    }
}