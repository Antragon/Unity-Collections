namespace Collections.Ranges {
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public static class RangeExtensions {
        public static bool Contains<T>(this IRange<T> range, T value)
        where T : IComparable<T> {
            return value.CompareTo(range.Start) >= 0 && value.CompareTo(range.End) <= 0;
        }

        public static bool Overlap<T>(this IRange<T> range, IRange<T> value)
        where T : IComparable<T> {
            return value.End.CompareTo(range.Start) >= 0 && value.Start.CompareTo(range.End) <= 0;
        }

        public static bool Overlap<T>(this IRange<T> range, IEnumerable<T> values)
        where T : IComparable<T> {
            
            return values.Any(range.Contains);
        }

        public static T Clamp<T>(this IRange<T> range, T value)
        where T : IComparable<T> {
            if (value.CompareTo(range.Start) < 0) {
                return range.Start;
            }

            if (value.CompareTo(range.End) > 0) {
                return range.End;
            }

            return value;
        }
    }
}