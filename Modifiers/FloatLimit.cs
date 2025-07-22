namespace Collections.Modifiers {
    using System.Collections.Generic;
    using System.Linq;

    public class FloatLimit : Dictionary<object, float> {
        public float? Value => Count > 0 ? Values.Min() : null;
    }
}