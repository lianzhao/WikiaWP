using System.Collections.Generic;

namespace Wikia.RelatedPages
{
    public class RelatedPagesResultSet
    {
        public IDictionary<int, RelatedPage[]> items { get; set; }
        public string basepath { get; set; }
    }
}