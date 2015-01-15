using System;
using System.Collections.Generic;
using System.Linq;

namespace LianZhao.Collections.Generic
{
    public static class CollectionExtensions
    {
        public static void AddRange<T>(this ICollection<T> collection, IEnumerable<T> items)
        {
            if (collection is List<T>)
            {
                (collection as List<T>).AddRange(items);
                return;
            }

            foreach (var item in items)
            {
                collection.Add(item);
            }
        }

        public static void AddRange<T>(this ICollection<T> collection, params T[] items)
        {
            collection.AddRange(items.AsEnumerable());
        }

        public static void RemoveRange<T>(this ICollection<T> collection, Func<T, bool> predicate)
        {
            var items = collection.Where(predicate).ToArray();
            foreach (var item in items)
            {
                collection.Remove(item);
            }
        }
    }
}