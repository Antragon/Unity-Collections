namespace Collections.Ranges {
    using System;
    using UnityEngine;

    [Serializable]
    public class Range<T>
    where T : IComparable {
        public Range(T start = default, T end = default) {
            Start = start;
            End = end;
        }

        [field: SerializeField] public T Start { get; private set; }
        [field: SerializeField] public T End { get; private set; }

        public bool Contains(T value) {
            return value.CompareTo(Start) >= 0 && value.CompareTo(End) <= 0;
        }

        public T Clamp(T value) {
            if (value.CompareTo(Start) < 0) {
                return Start;
            }

            if (value.CompareTo(End) > 0) {
                return End;
            }

            return value;
        }

        public override string ToString() {
            return $"{Start} - {End}";
        }
    }
}