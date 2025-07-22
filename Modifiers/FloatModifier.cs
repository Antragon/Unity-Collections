namespace Collections.Modifiers {
    using System.Collections.Generic;
    using System.Linq;

    public class FloatModifier : Dictionary<object, float> {
        private readonly float _defaultValue;

        public FloatModifier(float defaultValue = 1) {
            _defaultValue = defaultValue;
        }

        public float Value => Count > 0 ? Values.Aggregate((x, y) => x * y) : _defaultValue;
    }
}