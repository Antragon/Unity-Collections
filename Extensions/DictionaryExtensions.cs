namespace Collections.Extensions {
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Object = UnityEngine.Object;

    public static class DictionaryExtensions {
        public static bool TryAdd<TKey, TValue>(this Dictionary<TKey, TValue> dict, TKey key, Func<TValue> value) {
            if (!dict.ContainsKey(key)) {
                dict.Add(key, value());
                return true;
            }

            return false;
        }

        public static Dictionary<string, TValue> ToDictionary<TValue>(this IEnumerable<TValue> enumerable) where TValue : Object {
            return enumerable.ToDictionary(x => x.name, x => x);
        }
    }
}