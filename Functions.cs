namespace Collections {
    using UnityEngine;

    public static class Functions {
        public static bool TryDestroyAndMarkAsDestroyed(this GameObject objectToDestroy) {
            if (objectToDestroy.IsDestroyed()) return false;

            objectToDestroy.tag = Tags.Destroyed;
            Object.Destroy(objectToDestroy);
            return true;
        }

        public static bool IsDestroyed(this GameObject gameObject) {
            return !gameObject || gameObject.CompareTag(Tags.Destroyed);
        }
    }
}