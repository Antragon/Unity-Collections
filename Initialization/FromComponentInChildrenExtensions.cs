namespace Collections.Initialization {
    using System.Reflection;
    using UnityEngine;

    public static class FromComponentInChildrenExtensions {
        public static void SetFieldFromComponentInChildren(this Component instance, FieldInfo field) {
            var component = instance.GetComponentInChildren(field.FieldType, true);
            instance.SetComponentValue(field, component);
        }

        public static void SetFieldFromComponentsInChildren(this Component instance, FieldInfo field) {
            var elementType = field.FieldType.GetElementType();
            const BindingFlags bindingFlags = BindingFlags.Static | BindingFlags.NonPublic;
            var method = typeof(FromComponentInChildrenExtensions).GetMethod(nameof(SetFieldFromComponentsInChildrenGeneric), bindingFlags);
            var genericMethod = method!.MakeGenericMethod(elementType);
            genericMethod.Invoke(null, new object[] { instance, field });
        }

        private static void SetFieldFromComponentsInChildrenGeneric<TComponent>(Component instance, FieldInfo field) {
            var components = instance.GetComponentsInChildren<TComponent>(true);
            instance.SetComponentValue(field, components);
        }
    }
}