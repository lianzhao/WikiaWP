using System;
using System.Collections.Generic;

namespace LianZhao.Linq
{
    public static partial class Enumerable
    {
        public static IEnumerable<TSource> Concat<TSource>(this IEnumerable<TSource> source, TSource element)
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }

            foreach (var e in source)
            {
                yield return e;
            }
            yield return element;
        }
}
}