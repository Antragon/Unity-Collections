namespace Collections.Nullables {
    using System;
    using UnityEngine;

    [Serializable]
    public struct NullableFloatWrapper : INullableWrapper<float> {
        [field: SerializeField] public float Value { get; set; }
        [field: SerializeField] public bool HasValue { get; set; }

        public static implicit operator float?(NullableFloatWrapper wrapper) {
            if (!wrapper.HasValue) {
                return null;
            }

            return wrapper.Value;
        }

        public static implicit operator NullableFloatWrapper(float value) {
            var wrapper = new NullableFloatWrapper { Value = value, HasValue = true };
            return wrapper;
        }

        public static implicit operator NullableFloatWrapper(float? value) {
            var wrapper = new NullableFloatWrapper { Value = value ?? default, HasValue = true };
            return wrapper;
        }
    }
}