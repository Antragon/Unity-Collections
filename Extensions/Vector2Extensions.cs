namespace Collections.Extensions {
    using System;
    using System.Collections;
    using UnityEngine;

    public static class Vector2Extensions {
        public static IEnumerator SmoothLerpTowards(this Vector2 start, Vector2 target, float speed, Action<Vector2> update, Func<bool> breakCondition = null) {
            var time = 0f;
            while (time < 1) {
                if (breakCondition?.Invoke() ?? false) yield break;
                time += speed * Time.deltaTime;
                var position = SmoothStep(start, target, time);
                update(position);
                yield return null;
            }

            update(target);
        }

        public static Vector2 SmoothStep(Vector2 from, Vector2 to, float t) {
            var step = Mathf.SmoothStep(0, 1, t);
            return Vector2.Lerp(from, to, step);
        }
        
        public static Vector2 Clamp(this Vector2 value, Vector2 min, Vector2 max) {
            var clampX = Mathf.Clamp(value.x, min.x, max.x);
            var clampY = Mathf.Clamp(value.y, min.y, max.y);
            return new Vector2(clampX, clampY);
        }

        public static bool IsOrthogonal(this Vector2 value) {
            var orthogonal = value.x == 0 || value.y == 0;
            return orthogonal;
        }
    }
}