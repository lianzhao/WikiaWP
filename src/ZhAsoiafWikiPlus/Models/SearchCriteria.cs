namespace ZhAsoiafWikiaApiPlus.Models
{
    public class SearchCriteria
    {
        public string Keyword { get; set; }

        public int Page { get; set; }

        public int? PageSize { get; set; }

        public bool IsValidRequest()
        {
            return !string.IsNullOrEmpty(Keyword) && Page >= 0;
        }

        public bool IsExactSearch()
        {
            return Page == 0;
        }
    }
}