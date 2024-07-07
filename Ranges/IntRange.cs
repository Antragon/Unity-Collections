namespace Collections.Ranges {
    using System;
    using System.Collections.Generic;
    using System.Linq;

    [Serializable]
    public class IntRange : Range<int> {
        public IntRange(int start = default, int end = default)
            : base(start, end) { }

        public IEnumerable<int> All => Enumerable.Range(Start, End - Start + 1);
    }
}