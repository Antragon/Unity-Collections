namespace Collections.Extensions {
    using System;
    using System.Linq;
    using UnityEngine;

    public static class ColliderExtensions {
        public static Vector2 GetSize(this Collider2D collider) {
            return collider switch {
                BoxCollider2D box => box.size,
                CircleCollider2D circle => Vector2.one * circle.radius * 2,
                PolygonCollider2D polygon => GetPolygonBounds(polygon),
                _ => throw new ArgumentOutOfRangeException()
            };
        }

        private static Vector2 GetPolygonBounds(PolygonCollider2D polygon) {
            var xMin = polygon.points.Min(p => p.x);
            var xMax = polygon.points.Max(p => p.x);
            var yMin = polygon.points.Min(p => p.y);
            var yMax = polygon.points.Max(p => p.y);
            var leftLower = new Vector2(xMin, yMin);
            var rightUpper = new Vector2(xMax, yMax);
            var size = rightUpper - leftLower;
            return size;
        }

        public static bool Contains2D(this Bounds bounds, Vector2 point) {
            var center = (Vector2)bounds.center;
            var size = (Vector2)bounds.size;
            var correctedBounds = new Bounds(center, size);
            return correctedBounds.Contains(point);
        }
    }
}