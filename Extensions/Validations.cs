namespace Collections.Extensions {
    using System.Linq;
    using UnityEngine;

    public static class Validations {
        public static void AssertNotNull(params object?[] objects) {
            if (objects.Any(x => x is Object @object && !@object || x == null)) {
                Debug.LogError("Object reference not set to an instance of an object");
            }
        }
    }
}