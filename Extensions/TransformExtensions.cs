namespace Collections.Extensions {
    using System;
    using System.Collections;
    using UnityEngine;

    public static class TransformExtensions {
        public static Transform GetSecondRoot(this Transform transform) {
            while (true) {
                if (transform == transform.root) {
                    return null;
                }

                if (transform.parent == transform.root) {
                    return transform;
                }

                transform = transform.parent;
            }
        }

        public static IEnumerator SmoothLerpTowards(this Transform transform, Vector2 target, float speed, Func<bool> breakCondition = null) {
            var start = (Vector2)transform.position;
            yield return start.SmoothLerpTowards(target, speed, transform.SetPosition2D, breakCondition);
        }

        public static void SetPosition2D(this Transform transform, Vector2 position2D) {
            var position = new Vector3(position2D.x, position2D.y, transform.position.z);
            transform.position = position;
        }

        public static void SetXPosition(this Transform transform, float x) {
            var position = transform.position;
            position.x = x;
            transform.position = position;
        }

        public static void SetYPosition(this Transform transform, float y) {
            var position = transform.position;
            position.y = y;
            transform.position = position;
        }

        public static void SetZRotation(this Transform transform, float z) {
            var angles = transform.rotation.eulerAngles;
            angles.z = z;
            transform.rotation = Quaternion.Euler(angles);
        }

        public static void SetLocalPosition2D(this Transform transform, Vector2 position2D) {
            var position = new Vector3(position2D.x, position2D.y, transform.localPosition.z);
            transform.localPosition = position;
        }

        public static void SetLocalXPosition(this Transform transform, float x) {
            var position = transform.localPosition;
            position.x = x;
            transform.localPosition = position;
        }

        public static void SetLocalYPosition(this Transform transform, float y) {
            var position = transform.localPosition;
            position.y = y;
            transform.localPosition = position;
        }

        public static void SetLocalScale2D(this Transform transform, Vector2 scale) {
            transform.SetLocalScale2D(scale.x, scale.y);
        }

        public static void SetLocalScale2D(this Transform transform, float x, float y) {
            var scale = new Vector3(x, y, transform.localScale.z);
            transform.localScale = scale;
        }

        public static void SetLocalXScale(this Transform transform, float x) {
            var scale = transform.localScale;
            scale.x = x;
            transform.localScale = scale;
        }

        public static void SetLocalYScale(this Transform transform, float y) {
            var scale = transform.localScale;
            scale.y = y;
            transform.localScale = scale;
        }
    }
}