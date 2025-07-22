namespace Collections.Extensions {
    using UnityEngine;

    public static class GameObjectExtensions {
        public static void EnableComponent<T>(this GameObject gameObject, bool value)
        where T : Behaviour {
            gameObject.GetComponent<T>().Enable(value);
        }
    }
}