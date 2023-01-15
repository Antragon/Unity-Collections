namespace Collections.Components {
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using UnityEngine;
    using Object = UnityEngine.Object;

    [DisallowMultipleComponent]
    [DefaultExecutionOrder(int.MinValue)]
    public class SingletonMarker : MonoBehaviour {
        private static readonly Dictionary<string, GameObject> _instances = new();
        private static readonly Dictionary<Type, object> _cachedComponents = new();

        public static T GetComponentInSingletons<T>() => (T)GetComponentInSingletons(typeof(T));

        public static object GetComponentInSingletons(Type type) {
            if (_cachedComponents.TryGetValue(type, out var component) && IsValidComponent(component)) {
                return component;
            }

            component = _instances.Values
                .Select(i => i.GetComponentInChildren(type, true))
                .FirstOrDefault(c => c != null);

            if (component != null) {
                _cachedComponents[type] = component;
            }

            return component;
        }

        private static bool IsValidComponent(object component) {
            return component switch {
                null => false,
                Object @object when !@object => false,
                _ => true
            };
        }

        public static object GetComponentInSingletons(Type type, string tag) {
            if (_instances.TryGetValue(tag, out var instance)) {
                return instance.GetComponentInChildren(type);
            }

            Debug.LogWarning($"Singleton not found ({tag})");
            return null;
        }

        private void Awake() {
            if (_instances.ContainsKey(tag)) {
                gameObject.SetActive(false);
                DestroyAndLog(gameObject, tag);
            } else {
                _instances.Add(tag, gameObject);
            }
        }

        private static void DestroyAndLog(GameObject toDestroy, string tag) {
            if (toDestroy.TryDestroyAndMarkAsDestroyed()) {
                Debug.Log($"Destroyed {toDestroy.name} due to duplication ({tag})");
            }
        }

        private void OnDestroy() {
            _instances.Remove(tag);
        }
    }
}