using System;
using System.Collections.Generic;

namespace LianZhao.Collections.Generic
{
    public static class DictionaryExtensions
    {
        public static bool TryGetValue<TValue>(
            this IDictionary<string, TValue> dict,
            string key,
            out TValue value,
            StringComparison comparisonType)
        {
            var rv = false;
            value = default(TValue);
            foreach (var kvp in dict)
            {
                if (kvp.Key.Equals(key, comparisonType))
                {
                    value = kvp.Value;
                    rv = true;
                    break;
                }
            }

            return rv;
        }

        public static TValue GetValue<TValue>(
            this IDictionary<string, TValue> dict,
            string key,
            StringComparison comparisonType)
        {
            foreach (var kvp in dict)
            {
                if (kvp.Key.Equals(key, comparisonType))
                {
                    return kvp.Value;
                }
            }
            return default(TValue);
        }
    }
}