using System;
using System.Net.Http;

using LianZhao;

using ZhAsoiafWiki.Categories;
using ZhAsoiafWiki.Dictionaries;
using ZhAsoiafWiki.Pages;

namespace ZhAsoiafWiki
{
    public class ApiClient : DisposableObjectOwner
    {
        public const string Site = "http://zh.asoiaf.wikia.com";

        private readonly HttpClient _httpClient;

        private readonly Lazy<DictionariesApiClient> _dictionariesLazy;

        private readonly Lazy<CategoriesApiClient> _categoriesLazy;

        private readonly Lazy<PagesApiClient> _pagesLazy;

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
            _pagesLazy = new Lazy<PagesApiClient>(() => new PagesApiClient(_httpClient, isOwner: false));
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

        public PagesApiClient Pages
        {
            get
            {
                return _pagesLazy.Value;
            }
        }
    }
}