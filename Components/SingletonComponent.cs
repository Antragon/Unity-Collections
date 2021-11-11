namespace Collections.Components {
    using System.Linq;
    using UnityEngine;

    [DisallowMultipleComponent]
    public abstract class SingletonComponent<T> : MonoBehaviour
    where T : SingletonComponent<T> {
        // ReSharper disable once MergeConditionalExpression
        public static T Self => ReferenceEquals(_self, null) ? _self = FindSingleton() : _self;

        private static T _self;

        private static T FindSingleton() {
            var foundSingletons = FindObjectsOfType<T>();
            if (foundSingletons.Length == 0) {
                Debug.LogWarning($"Singleton not found ({typeof(T)})");
                return null;
            }

            foreach (var singleton in foundSingletons.Skip(1)) {
                DestroyAndLog(singleton.gameObject);
            }

            return foundSingletons[0];
        }

        private static void DestroyAndLog(GameObject objectToDestroy) {
            if (Functions.TryDestroyAndMarkAsDestroyed(objectToDestroy)) {
                Debug.Log($"Destroyed {objectToDestroy.name} due to duplicated singletons ({typeof(T)})");
            }
        }

        protected void Awake() {
            if (Self != this) {
                DestroyAndLog(gameObject);
                return;
            }

            AwakeExtended();
        }

        protected virtual void AwakeExtended() { }

        protected void OnDestroy() {
            if (_self == this) _self = null;
            else return;

            OnDestroyExtended();
        }

        protected virtual void OnDestroyExtended() { }
    }
}