namespace Collections.Extensions {
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public static class EnumerableExtensions {
        public static bool In<T>(this T value, params T[] enumerable) {
            return enumerable.Contains(value);
        }

        public static bool In<T>(this T value, IEnumerable<T> enumerable) {
            return enumerable.Contains(value);
        }

        public static T Random<T>(this IList<T> enumerable) {
            var index = UnityEngine.Random.Range(0, enumerable.Count);
            return enumerable[index];
        }
        
        public static IEnumerable<T> Do<T>(this IEnumerable<T> enumerable, Action<T> action) {
            foreach (var item in enumerable) {
                action(item);
                yield return item;
            }
        }
    }
}