namespace Collections.Components {
    using System;
    using System.Collections.Generic;
    using UnityEngine;

    public class ComponentCache {
        private readonly Dictionary<Type, object> _cache = new();

        public ComponentCache(GameObject gameObject) {
            GameObject = gameObject;
        }

        public GameObject GameObject { get; }

        public T Get<T>() {
            if (!_cache.TryGetValue(typeof(T), out var component)) {
                component = GameObject.GetComponent<T>();
                _cache.Add(typeof(T), component);
            }

            return (T)component;
        }

        public static ComponentCache Create<T>(T baseComponent)
        where T : Component {
            var componentCache = new ComponentCache(baseComponent.gameObject);
            componentCache._cache.Add(typeof(T), baseComponent);
            return componentCache;
        }
    }
}