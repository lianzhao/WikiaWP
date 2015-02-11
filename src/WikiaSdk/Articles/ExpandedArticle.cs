namespace Wikia.Articles
{
    public class ExpandedArticle
    {
        public int id { get; set; }
        public string title { get; set; }
        public int ns { get; set; }
        public string url { get; set; }
        public Revision revision { get; set; }
        public int comments { get; set; }
        public string type { get; set; }
        public string @abstract { get; set; }
        public string thumbnail { get; set; }
        public Original_Dimensions original_dimensions { get; set; }

        public string GetOriginalImageUri()
        {
            return WikiaImageFormatter.ToOriginalImageUri(thumbnail);
        }

        public string GetCustomSquareImageThumbnailUri(int size = 0)
        {
            return string.IsNullOrEmpty(thumbnail) || original_dimensions == null
                       ? thumbnail
                       : WikiaImageFormatter.ToSquareImageThumbnailUri(
                           thumbnail,
                           original_dimensions.width,
                           original_dimensions.height,
                           size);
        }
    }
}