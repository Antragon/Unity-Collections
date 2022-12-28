﻿namespace Collections.Components {
    using System.Collections.Generic;
    using System.Linq;
    using UnityEngine;

    public class InstanceFactory : MonoBehaviour {
        private readonly Dictionary<GameObject, Object> _instancePrefabs = new();
        private readonly Dictionary<Object, HashSet<GameObject>> _instanceStorage = new();

        private void OnDestroy() {
            Clear();
        }

        public void Clear() {
            foreach (var instance in _instancePrefabs.Keys) {
                instance.TryDestroyAndMarkAsDestroyed();
            }

            _instancePrefabs.Clear();
            _instanceStorage.Clear();
        }

        public void Store(GameObject instance) {
            if (!_instancePrefabs.TryGetValue(instance, out var prefab)) {
                Debug.LogWarning($"Instance {instance} must be created by this factory to be stored here");
                return;
            }

            _instanceStorage.TryAdd(prefab, new HashSet<GameObject>());
            _instanceStorage[prefab].Add(instance);
            instance.SetActive(false);
        }

        public GameObject CreateInstance(GameObject prefab, Transform parent = null, Vector2 position = default, Quaternion rotation = default) {
            return CreateInstance(prefab, prefab.name, parent, position, rotation);
        }

        public GameObject CreateInstance(GameObject prefab, string instanceName, Transform parent = null, Vector2 position = default, Quaternion rotation = default) {
            var instance = GetStoredInstance(prefab);
            if (!instance) {
                instance = Instantiate(prefab, parent);
                _instancePrefabs.Add(instance, prefab);
            }

            instance.name = instanceName;
            instance.transform.SetParent(parent);
            instance.transform.localPosition = position;
            instance.transform.rotation = rotation;
            return instance;
        }

        private GameObject GetStoredInstance(Object prefab) {
            if (!_instanceStorage.TryGetValue(prefab, out var instances)) {
                return null;
            }

            var instance = instances.FirstOrDefault();
            instances.Remove(instance);
            if (instance) {
                instance.SetActive(true);
            }

            return instance;
        }
    }
}