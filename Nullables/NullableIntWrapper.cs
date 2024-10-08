﻿namespace Collections.Nullables {
    using System;
    using UnityEngine;

    [Serializable]
    public struct NullableIntWrapper : INullableWrapper<int> {
        [field: SerializeField] public int Value { get; set; }
        [field: SerializeField] public bool HasValue { get; set; }

        public static implicit operator int?(NullableIntWrapper wrapper) {
            if (!wrapper.HasValue) {
                return null;
            }

            return wrapper.Value;
        }

        public static implicit operator NullableIntWrapper(int value) {
            var wrapper = new NullableIntWrapper { Value = value, HasValue = true };
            return wrapper;
        }

        public static implicit operator NullableIntWrapper(int? value) {
            var wrapper = new NullableIntWrapper { Value = value ?? default, HasValue = value.HasValue };
            return wrapper;
        }
    }
}