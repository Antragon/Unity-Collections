namespace Collections.Components {
    using System.Collections.Generic;
    using System.Linq;
    using UnityEngine;

    [DisallowMultipleComponent]
    public class SingletonMarker : MonoBehaviour {
        private static readonly Dictionary<string, GameObject> _instances = new();

        public static GameObject GetInstance(string tag) {
            var instance = _instances.ContainsKey(tag) ? _instances[tag] : FindInstance(tag);
            return instance;
        }

        private static GameObject FindInstance(string tag) {
            var instances = GameObject.FindGameObjectsWithTag(tag);
            if (instances.Length == 0) {
                Debug.LogWarning($"Singleton not found ({tag})");
                return null;
            }

            foreach (var toDestroy in instances.Skip(1)) {
                DestroyAndLog(toDestroy, tag);
            }

            var instance = instances[0];
            _instances.Add(tag, instance);
            return instance;
        }

        private static void DestroyAndLog(GameObject toDestroy, string tag) {
            if (toDestroy.TryDestroyAndMarkAsDestroyed()) {
                Debug.Log($"Destroyed {toDestroy.name} due to duplication ({tag})");
            }
        }

        private void Awake() {
            if (_instances.ContainsKey(tag)) {
                if (_instances[tag] != gameObject) {
                    DestroyAndLog(gameObject, tag);
                }
            } else {
                _instances.Add(tag, gameObject);
            }
        }

        private void OnDestroy() {
            _instances.Remove(tag);
        }
    }
}