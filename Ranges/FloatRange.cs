﻿namespace Collections.Ranges {
    using System;
    using Random = UnityEngine.Random;

    [Serializable]
    public class FloatRange : Range<float> {
        public FloatRange(float start = default, float end = default)
            : base(start, end) { }

        public float GetRandom() => Random.Range(Start, End);
    }
}