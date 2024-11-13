namespace Collections.Ranges {
    using System;
    using UnityEngine;
    using Random = UnityEngine.Random;

    [Serializable]
    public struct FloatRange : IRange<float> {
        public FloatRange(float start = default, float end = default) {
            Start = start;
            End = end;
        }

        [field: SerializeField] public float Start { get; private set; }
        [field: SerializeField] public float End { get; private set; }

        public float GetRandom() => Random.Range(Start, End);

        public float InverseLerp(float value) {
            return Mathf.InverseLerp(Start, End, value);
        }

        public override string ToString() {
            return $"{Start} - {End}";
        }
    }
}