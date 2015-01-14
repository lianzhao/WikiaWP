using System.Collections.Generic;
using System.Linq;

namespace LianZhao.Patterns.Composite
{
    public static class Composites
    {
        public static IEnumerable<T> Flatten<T>(this IComposite<T> composite)
        {
            return
                composite.Items.OfType<IComposite<T>>()
                         .Aggregate(Enumerable.Empty<T>(), (current, newItem) => current.Concat(newItem.Flatten()))
                         .Concat(composite.Items.Where(item => !(item is IComposite<T>)));
        }
    }
}