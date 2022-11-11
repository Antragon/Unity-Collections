namespace Collections.Initialization {
    using System.Reflection;
    using Components;
    using UnityEngine;

    public static class FromComponentInChildrenExtensions {
        public static void SetFieldFromComponentInChildren(this Component instance, FieldInfo field) {
            var attribute = field.GetCustomAttribute<FromComponentInChildrenAttribute>();
            var componentProvider = string.IsNullOrEmpty(attribute.SingletonTag) ?
                instance :
                SingletonMarker.GetInstance(attribute.SingletonTag).transform;
            var component = componentProvider.GetComponentInChildren(field.FieldType, true);
            instance.SetComponentValue(field, component);
        }

        public static void SetFieldFromComponentsInChildren(this Component instance, FieldInfo field) {
            var attribute = field.GetCustomAttribute<FromComponentsInChildrenAttribute>();
            var componentProvider = string.IsNullOrEmpty(attribute.SingletonTag) ?
                instance :
                SingletonMarker.GetInstance(attribute.SingletonTag).transform;
            var elementType = field.FieldType.GetElementType();
            const BindingFlags bindingFlags = BindingFlags.Static | BindingFlags.NonPublic;
            var method = typeof(FromComponentInChildrenExtensions).GetMethod(nameof(SetFieldFromComponentsInChildrenGeneric), bindingFlags);
            var genericMethod = method!.MakeGenericMethod(elementType);
            genericMethod.Invoke(null, new object[] { componentProvider, instance, field });
        }

        private static void SetFieldFromComponentsInChildrenGeneric<TComponent>(Component componentProvider, Component instance, FieldInfo field) {
            var components = componentProvider.GetComponentsInChildren<TComponent>(true);
            instance.SetComponentValue(field, components);
        }
    }
}