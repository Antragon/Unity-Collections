namespace Collections.Initialization {
    using System.Reflection;
    using UnityEngine;

    public static class FromComponentInParentExtensions {
        public static void SetFieldFromComponentInParent(this object instance, FieldInfo field, Component componentProvider) {
            var component = componentProvider.GetComponentInParent(field.FieldType, true);
            instance.SetComponentValue(field, component);
        }

        public static void SetFieldFromComponentsInParent(this object instance, FieldInfo field, Component componentProvider) {
            var elementType = field.FieldType.GetElementType();
            const BindingFlags bindingFlags = BindingFlags.Static | BindingFlags.NonPublic;
            var method = typeof(FromComponentInParentExtensions).GetMethod(nameof(SetFieldFromComponentsInParentGeneric), bindingFlags);
            var genericMethod = method!.MakeGenericMethod(elementType);
            genericMethod.Invoke(null, new[] { instance, field, componentProvider });
        }

        private static void SetFieldFromComponentsInParentGeneric<TComponent>(object instance, FieldInfo field, Component componentProvider) {
            var components = componentProvider.GetComponentsInParent<TComponent>(true);
            instance.SetComponentValue(field, components);
        }
    }
}