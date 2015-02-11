using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace Wikia
{
    public static class WikiaImageFormatter
    {
        private const string FixWikiaBugPattern = @"y-offset/-(\w+)";

        private const string FixWikiaBugReplace = @"y-offset/0";

        private const string OriginalImagePattern = @"\/latest.*path-prefix";

        private const string OriginalImageReplace = @"/latest?path-prefix";

        private const string ImageThumbnailPattern = @"\/latest.*path-prefix";

        public static string ToImageThumbnailUri(string imageUri, int width)
        {
            return string.IsNullOrEmpty(imageUri)
                       ? imageUri
                       : Regex.Replace(
                           imageUri,
                           ImageThumbnailPattern,
                           string.Format(
                               @"/latest/scale-to-width/{0}?path-prefix",
                               width.ToString(CultureInfo.InvariantCulture)));
        }

        public static string ToImageThumbnailUri(
            string imageUri,
            int windowCropWidth,
            int windowWidth,
            int windowHeight,
            int xOffset = 0,
            int yOffset = 0)
        {
            if (string.IsNullOrEmpty(imageUri))
            {
                return imageUri;
            }

            var sb =
                new StringBuilder(@"/latest/window-crop/width/").Append(windowCropWidth)
                    .Append(@"/x-offset/")
                    .Append(xOffset)
                    .Append(@"/y-offset/")
                    .Append(yOffset)
                    .Append(@"/window-width/")
                    .Append(windowWidth)
                    .Append(@"/window-height/")
                    .Append(windowHeight)
                    .Append(@"?path-prefix");
            return Regex.Replace(imageUri, ImageThumbnailPattern, sb.ToString());
        }

        public static string ToSquareImageThumbnailUri(
            string imageUri,
            int originalWidth,
            int originalHeight,
            int toSize = 0)
        {
            if (string.IsNullOrEmpty(imageUri))
            {
                return imageUri;
            }

            if (originalHeight > originalWidth)
            {
                if (toSize <= 0)
                {
                    toSize = originalWidth;
                }

                return ToImageThumbnailUri(imageUri, toSize, originalWidth, originalWidth);
            }
            else
            {
                if (toSize <= 0)
                {
                    toSize = originalHeight;
                }

                var offset = (originalWidth - originalHeight) / 2;
                return ToImageThumbnailUri(imageUri, toSize, originalHeight, originalHeight, offset);
            }
        }

        public static string FixWikiaImageThumbnailBug(string imageThumbnailUri)
        {
            return string.IsNullOrEmpty(imageThumbnailUri)
                       ? imageThumbnailUri
                       : Regex.Replace(imageThumbnailUri, FixWikiaBugPattern, FixWikiaBugReplace);
        }

        public static string ToOriginalImageUri(string imageThumbnailUri)
        {
            return string.IsNullOrEmpty(imageThumbnailUri)
                       ? imageThumbnailUri
                       : Regex.Replace(imageThumbnailUri, OriginalImagePattern, OriginalImageReplace);
        }
    }
}