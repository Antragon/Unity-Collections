namespace Collections.Components {
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using UnityEngine;

    public class ComponentCache : IEquatable<ComponentCache> {
        private readonly Dictionary<Type, object?> _cache = new();

        public ComponentCache(GameObject gameObject) {
            GameObject = gameObject;
        }

        public GameObject GameObject { get; }

        public bool TryGet<T>([NotNullWhen(true)] out T? component) {
            component = Get<T>();
            return component != null;
        }

        public T? Get<T>() {
            if (!_cache.TryGetValue(typeof(T), out var component)) {
                component = GameObject.GetComponent<T>();
                _cache.Add(typeof(T), component);
            }

            return (T?)component;
        }

        public static ComponentCache Create<T>(T baseComponent)
        where T : Component {
            var componentCache = new ComponentCache(baseComponent.gameObject);
            componentCache._cache.Add(typeof(T), baseComponent);
            return componentCache;
        }

        public bool Equals(ComponentCache? other) {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;
            return GameObject.Equals(other.GameObject);
        }

        public override bool Equals(object? obj) {
            if (obj is null) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((ComponentCache)obj);
        }

        public override int GetHashCode() {
            return HashCode.Combine(GameObject);
        }
    }
}