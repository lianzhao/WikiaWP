using System.Globalization;
using System.Text.RegularExpressions;

namespace Wikia.Mercury
{
    public class Relatedpage
    {
        public const string WindowWidthReplacePattern = @"window-width/(\w+)";

        public const string WindowHeightReplacePattern = @"window-height/(\w+)";

        public const string YOffsetReplacePattern = @"y-offset/(\w+)";

        public const string WindowWidthReplaceFormat = @"window-width/{0}";

        public const string WindowHeightReplaceFormat = @"window-height/{0}";

        public const string YOffsetReplace = @"y-offset/0";

        public static readonly int WindowWidthLength = @"window-width/".Length;

        public string url { get; set; }
        public string title { get; set; }
        public int id { get; set; }
        public string imgUrl { get; set; }
        public Imgoriginaldimensions imgOriginalDimensions { get; set; }
        public string text { get; set; }

        public string FixImgUrl()
        {
            if (string.IsNullOrEmpty(imgUrl))
            {
                return imgUrl;
            }

            var rv = Regex.Replace(imgUrl, YOffsetReplacePattern, YOffsetReplace);
            var match = Regex.Match(rv, WindowWidthReplacePattern);
            if (match != null && match.Success)
            {
                rv = Regex.Replace(
                    rv,
                    WindowHeightReplacePattern,
                    string.Format(WindowHeightReplaceFormat, match.Value.Substring(WindowWidthLength)));
            }
            return rv;
        }
    }
}