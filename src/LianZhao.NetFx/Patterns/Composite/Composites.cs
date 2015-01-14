using System.Collections.Generic;
using System.Linq;

namespace LianZhao.Patterns.Composite
{
    public static class Composites
    {
        public static IEnumerable<T> GetAllDescendants<T>(this IComposite<T> composite)
        {
            return composite.Items == null
                       ? Enumerable.Empty<T>()
                       : composite.Items.OfType<IComposite<T>>()
                             .Aggregate(
                                 composite.Items.AsEnumerable(),
                                 (current, newItem) => current.Concat(newItem.GetAllDescendants()));
        }
    }
}