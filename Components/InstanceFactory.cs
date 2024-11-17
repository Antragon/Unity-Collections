namespace Collections.Components {
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Initialization;
    using UnityEngine;
    using Object = UnityEngine.Object;

    public class InstanceFactory : MonoBehaviour {
        [FromComponentInSingletons] private readonly ActionQueue _actionQueue = null!;

        private readonly Dictionary<GameObject, Object> _instancePrefabs = new();
        private readonly Dictionary<Object, Queue<GameObject>> _instanceStorage = new();
        private readonly List<IDisposable> _callbacks = new();

        private void Awake() {
            this.Initialize();
        }

        private void OnDestroy() {
            if (GameControl.ApplicationIsQuitting) return;
            Clear();
        }

        public void Clear() {
            foreach (var instance in _instancePrefabs.Keys) {
                instance.TryDestroyAndMarkAsDestroyed();
            }

            CancelPrewarming();
            _instancePrefabs.Clear();
            _instanceStorage.Clear();
        }

        public void Prewarm(GameObject prefab, Transform? parent = null, int count = 1) {
            if (_instanceStorage.TryGetValue(prefab, out var instances)) {
                count -= instances.Count;
                count = Mathf.Max(count, 0);
            }

            var callbacks = Enumerable.Range(0, count)
                .Select(_ => _actionQueue.Add(() => PrewarmInternal(prefab, parent)))
                .ToList();
            _callbacks.AddRange(callbacks);
        }

        private void PrewarmInternal(GameObject prefab, Transform? parent) {
            var instance = CreateInstanceInternal(prefab, parent);
            Store(instance);
        }

        public GameObject CreateInstance(GameObject prefab, Transform? parent = null) {
            CancelPrewarming();
            var instance = GetStoredInstance(prefab) ?? CreateInstanceInternal(prefab, parent);
            instance.transform.SetParent(parent);
            return instance;
        }

        private void CancelPrewarming() {
            _callbacks.ForEach(c => c.Dispose());
            _callbacks.Clear();
        }

        private GameObject CreateInstanceInternal(GameObject prefab, Transform? parent) {
            var instance = Instantiate(prefab, parent);
            instance.name = prefab.name;
            _instancePrefabs.Add(instance, prefab);
            return instance;
        }

        private GameObject? GetStoredInstance(Object prefab) {
            if (!_instanceStorage.TryGetValue(prefab, out var instances)) {
                return null;
            }

            if (instances.TryDequeue(out var instance)) {
                instance.SetActive(true);
            }

            return instance;
        }

        public void Store(GameObject instance) {
            if (!_instancePrefabs.TryGetValue(instance, out var prefab)) {
                Debug.LogWarning($"Instance {instance} must be created by this factory to be stored here");
                return;
            }

            _instanceStorage.TryAdd(prefab, new Queue<GameObject>());
            _instanceStorage[prefab].Enqueue(instance);
            instance.SetActive(false);
        }
    }
}