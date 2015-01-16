using System.Text.RegularExpressions;

namespace Wikia.Articles
{
    public class ExpandedArticle
    {
        public const string YOffsetReplacePattern = @"y-offset/-(\w+)";

        public const string YOffsetReplace = @"y-offset/0";

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

        public string ThumbnailFixYOffset
        {
            get
            {
                return thumbnail == null ? null : Regex.Replace(thumbnail, YOffsetReplacePattern, YOffsetReplace);
            }
        }
    }
}