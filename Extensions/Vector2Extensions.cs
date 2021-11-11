namespace Collections.Extensions {
    using UnityEngine;

    public static class Vector2Extensions {
        public static Vector2 Clamp(this Vector2 value, Vector2 min, Vector2 max) {
            var clampX = Mathf.Clamp(value.x, min.x, max.x);
            var clampY = Mathf.Clamp(value.y, min.y, max.y);
            return new Vector2(clampX, clampY);
        }
    }
}