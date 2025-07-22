namespace Collections.Extensions {
    using UnityEngine;

    public static class BehaviourExtensions {
        public static void Enable(this Behaviour behaviour, bool value) {
            behaviour.enabled = value;
        }

        public static void StopNullableCoroutine(this MonoBehaviour behaviour, Coroutine? coroutine) {
            if (coroutine != null) {
                behaviour.StopCoroutine(coroutine);
            }
        }
    }
}