namespace Collections.Initialization {
    using System.Reflection;
    using UnityEngine;

    public static class FromComponentInChildrenExtensions {
        public static void SetFieldFromComponentInChildren(this object instance, FieldInfo field, Component componentProvider) {
            var component = componentProvider.GetComponentInChildren(field.FieldType, true);
            instance.SetComponentValue(field, component);
        }

        public static void SetFieldFromComponentsInChildren(this object instance, FieldInfo field, Component componentProvider) {
            var elementType = field.FieldType.GetElementType();
            const BindingFlags bindingFlags = BindingFlags.Static | BindingFlags.NonPublic;
            var method = typeof(FromComponentInChildrenExtensions).GetMethod(nameof(SetFieldFromComponentsInChildrenGeneric), bindingFlags);
            var genericMethod = method!.MakeGenericMethod(elementType);
            genericMethod.Invoke(null, new[] { instance, field, componentProvider });
        }

        private static void SetFieldFromComponentsInChildrenGeneric<TComponent>(object instance, FieldInfo field, Component componentProvider) {
            var components = componentProvider.GetComponentsInChildren<TComponent>(true);
            instance.SetComponentValue(field, components);
        }
    }
}