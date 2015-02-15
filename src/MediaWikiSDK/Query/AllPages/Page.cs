using System;

namespace MediaWiki.Query.AllPages
{
    public class Page
    {
        public int pageid { get; set; }
        public int ns { get; set; }
        public string title { get; set; }
        public Category[] categories { get; set; }
        public Extlink[] extlinks { get; set; }
        public Image[] images { get; set; }
        public DateTime touched { get; set; }
        public int lastrevid { get; set; }
        public string counter { get; set; }
        public string redirect { get; set; }
        public string @new { get; set; }
        public int length { get; set; }
        public Langlink[] langlinks { get; set; }
        public Link[] links { get; set; }
        public Pageprops pageprops { get; set; }
        public Revision[] revisions { get; set; }
        public Template[] templates { get; set; }
    }
}