using System.Collections.Generic;
using System.Linq;

namespace LianZhao.Collections.Generic
{
    public class GroupingList<TKey, TElement> : List<TElement>, IGrouping<TKey, TElement>
    {
        public TKey Key { get; private set; }

        public GroupingList(TKey key, IEnumerable<TElement> collection)
            : base(collection)
        {
            Key = key;
        }

        public GroupingList(IGrouping<TKey, TElement> grouping)
            : this(grouping.Key, grouping)
        {
        }
    }
}