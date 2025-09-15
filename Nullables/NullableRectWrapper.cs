namespace Collections.Nullables {
    using System;
    using UnityEngine;

    [Serializable]
    public struct NullableRectWrapper {
        [field: SerializeField] public bool HasValue { get; private set; }
        [field: SerializeField] public Rect Value { get; private set; }

        public static implicit operator Rect?(NullableRectWrapper wrapper) {
            if (!wrapper.HasValue) {
                return null;
            }

            return wrapper.Value;
        }

        public static implicit operator NullableRectWrapper(Rect value) {
            var wrapper = new NullableRectWrapper { Value = value, HasValue = true };
            return wrapper;
        }

        public static implicit operator NullableRectWrapper(Rect? value) {
            var wrapper = new NullableRectWrapper { Value = value ?? default, HasValue = true };
            return wrapper;
        }
    }
}