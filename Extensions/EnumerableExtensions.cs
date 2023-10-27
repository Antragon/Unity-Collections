namespace Collections.Extensions {
    using System.Collections.Generic;
    using System.Linq;

    public static class EnumerableExtensions {
        public static bool In<T>(this T value, IEnumerable<T> enumerable) {
            return enumerable.Contains(value);
        }
    }
}