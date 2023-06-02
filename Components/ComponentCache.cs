namespace Collections.Components {
    using System;
    using System.Collections.Generic;
    using UnityEngine;

    public class ComponentCache {
        private readonly Component _baseComponent;
        private readonly Dictionary<Type, object> _cache = new();

        private ComponentCache(Component baseComponent) {
            _baseComponent = baseComponent;
        }

        public T Get<T>() {
            if (!_cache.TryGetValue(typeof(T), out var component)) {
                component = _baseComponent.GetComponent<T>();
                _cache.Add(typeof(T), component);
            }

            return (T)component;
        }

        public static ComponentCache Create<T>(T baseComponent)
        where T : Component {
            var componentCache = new ComponentCache(baseComponent);
            componentCache._cache.Add(typeof(T), baseComponent);
            return componentCache;
        }
    }
}