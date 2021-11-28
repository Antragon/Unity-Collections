namespace Collections.Extensions {
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
    }
}