using System;
using System.Net.Http;

using LianZhao;

using Wikia.Asoiaf.Zh.Categories;
using Wikia.Asoiaf.Zh.Dictionaries;

namespace Wikia.Asoiaf.Zh
{
    public class ApiClient : DisposableObjectOwner
    {
        public const string Site = "http://zh.asoiaf.wikia.com";

        private readonly HttpClient _httpClient;

        private readonly Lazy<DictionariesApiClient> _dictionariesLazy;

        private readonly Lazy<CategoriesApiClient> _categoriesLazy;

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
            _categoriesLazy = new Lazy<CategoriesApiClient>(() => new CategoriesApiClient(_httpClient, isOwner: false));
        }

        public DictionariesApiClient Dictionaries
        {
            get
            {
                return _dictionariesLazy.Value;
            }
        }

        public CategoriesApiClient Categories
        {
            get
            {
                return _categoriesLazy.Value;
            }
        }
    }
}