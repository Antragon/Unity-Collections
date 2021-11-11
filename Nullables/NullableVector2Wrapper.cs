namespace Collections.Nullables {
    using System;
    using UnityEngine;

    [Serializable]
    public struct NullableVector2Wrapper {
        [field: SerializeField] public Vector2 Value { get; private set; }
        [field: SerializeField] public bool HasValue { get; private set; }

        public static implicit operator Vector2?(NullableVector2Wrapper wrapper) {
            if (!wrapper.HasValue) {
                return null;
            }

            return wrapper.Value;
        }

        public static implicit operator NullableVector2Wrapper(Vector2 value) {
            var wrapper = new NullableVector2Wrapper { Value = value, HasValue = true };
            return wrapper;
        }

        public static implicit operator NullableVector2Wrapper(Vector2? value) {
            var wrapper = new NullableVector2Wrapper { Value = value ?? default, HasValue = true };
            return wrapper;
        }
    }
}