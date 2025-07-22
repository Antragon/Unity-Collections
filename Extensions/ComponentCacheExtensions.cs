namespace Collections.Extensions {
    using Components;
    using UnityEngine;

    public static class ComponentCacheExtensions {
        public static void Enable<T>(this ComponentCache gameObject, bool value)
        where T : Behaviour {
            gameObject.Get<T>()?.Enable(value);
        }

        public static ComponentCache? ToNullableComponentCache(this GameObject gameObject) {
            return gameObject ? new ComponentCache(gameObject) : null;
        }

        public static ComponentCache? ToNullableComponentCache<T>(this T baseComponent) where T : Component {
            return baseComponent ? ComponentCache.Create(baseComponent) : null;
        }
    }
}