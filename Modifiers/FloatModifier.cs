namespace Collections.Modifiers {
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;

    public class FloatModifier : Dictionary<object, float> {
        private readonly float _defaultValue;

        public FloatModifier(float defaultValue = 1) {
            _defaultValue = defaultValue;
        }

        public float Value {
            [SuppressMessage("ReSharper", "ForeachCanBeConvertedToQueryUsingAnotherGetEnumerator", Justification = "Allocation-free code")]
            get {
                if (Count == 0) return _defaultValue;
                var result = 1f;
                foreach (var value in Values) {
                    result *= value;
                }

                return result;
            }
        }
    }
}