using System.Collections.Generic;

namespace LianZhao.Linq
{
    public static partial class Enumerable
    {
        public static string JoinToString<TSource>(this IEnumerable<TSource> source, string separator)
        {
            return string.Join(separator, source);
        }
    }
}