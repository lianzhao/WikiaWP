namespace ZhAsoiafWikiaApiPlus.Models
{
    public static class ModelExtensions
    {
        public static bool IsValidRequest(this SearchCriteria model)
        {
            return !string.IsNullOrEmpty(model.Keyword) && model.Page >= 0;
        }

        public static bool IsExactSearch(this SearchCriteria model)
        {
            return model.Page == 0;
        }
    }
}