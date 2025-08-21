namespace Collections.Ranges {
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using UnityEngine;
    using Random = UnityEngine.Random;

    [Serializable]
    public struct IntRange : IRange<int> {
        public IntRange(int start = 0, int end = 0) {
            Start = start;
            End = end;
        }

        private int Min => Mathf.Min(Start, End);
        private int Max => Mathf.Max(Start, End);

        [field: SerializeField] public int Start { get; private set; }
        [field: SerializeField] public int End { get; private set; }

        public int GetRandomIncludingMax() => Random.Range(Min, Max + 1);

        public IEnumerable<int> All => Enumerable.Range(Min, Max - Min + 1);

        public override string ToString() {
            return $"{Start} - {End}";
        }
    }
}