namespace Collections.Initialization {
    using System.Reflection;
    using UnityEngine;

    public static class FromComponentExtensions {
        public static void SetFieldFromComponent(this object instance, FieldInfo field, Component componentProvider) {
            var component = componentProvider.GetComponent(field.FieldType);
            instance.SetComponentValue(field, component);
        }

        public static void SetFieldFromComponents(this object instance, FieldInfo field, Component componentProvider) {
            var elementType = field.FieldType.GetElementType();
            const BindingFlags bindingFlags = BindingFlags.Static | BindingFlags.NonPublic;
            var method = typeof(FromComponentExtensions).GetMethod(nameof(SetFieldFromComponentsGeneric), bindingFlags);
            var genericMethod = method!.MakeGenericMethod(elementType);
            genericMethod.Invoke(null, new[] { instance, field, componentProvider });
        }

        private static void SetFieldFromComponentsGeneric<TComponent>(object instance, FieldInfo field, Component componentProvider) {
            var components = componentProvider.GetComponents<TComponent>();
            instance.SetComponentValue(field, components);
        }
    }
}