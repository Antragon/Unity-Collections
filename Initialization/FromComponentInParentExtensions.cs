namespace Collections.Initialization {
    using System.Reflection;
    using Components;
    using UnityEngine;

    public static class FromComponentInParentExtensions {
        public static void SetFieldFromComponentInParent(this Component instance, FieldInfo field) {
            var attribute = field.GetCustomAttribute<FromComponentInParentAttribute>();
            var componentProvider = string.IsNullOrEmpty(attribute.SingletonTag) ?
                instance :
                SingletonMarker.GetInstance(attribute.SingletonTag).transform;
            var component = componentProvider.GetComponentInParent(field.FieldType);
            instance.SetComponentValue(field, component);
        }

        public static void SetFieldFromComponentsInParent(this Component instance, FieldInfo field) {
            var attribute = field.GetCustomAttribute<FromComponentsInParentAttribute>();
            var componentProvider = string.IsNullOrEmpty(attribute.SingletonTag) ?
                instance :
                SingletonMarker.GetInstance(attribute.SingletonTag).transform;
            var elementType = field.FieldType.GetElementType();
            const BindingFlags bindingFlags = BindingFlags.Static | BindingFlags.NonPublic;
            var method = typeof(FromComponentInParentExtensions).GetMethod(nameof(SetFieldFromComponentsInParentGeneric), bindingFlags);
            var genericMethod = method!.MakeGenericMethod(elementType);
            genericMethod.Invoke(null, new object[] { componentProvider, instance, field });
        }

        private static void SetFieldFromComponentsInParentGeneric<TComponent>(Component componentProvider, Component instance, FieldInfo field) {
            var components = componentProvider.GetComponentsInParent<TComponent>(true);
            instance.SetComponentValue(field, components);
        }
    }
}