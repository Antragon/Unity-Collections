namespace Collections.Ranges {
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using UnityEngine;

    [Serializable]
    public struct IntRange : IRange<int> {
        public IntRange(int start = 0, int end = 0) {
            Start = start;
            End = end;
        }

        [field: SerializeField] public int Start { get; private set; }
        [field: SerializeField] public int End { get; private set; }

        public IEnumerable<int> All => Enumerable.Range(Start, End - Start + 1);

        public override string ToString() {
            return $"{Start} - {End}";
        }
    }
}