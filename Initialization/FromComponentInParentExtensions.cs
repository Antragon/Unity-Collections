namespace Collections.Initialization {
    using System.Reflection;
    using UnityEngine;

    public static class FromComponentInParentExtensions {
        public static void SetFieldFromComponentInParent(this Component instance, FieldInfo field) {
            var component = instance.GetComponentInParent(field.FieldType, true);
            instance.SetComponentValue(field, component);
        }

        public static void SetFieldFromComponentsInParent(this Component instance, FieldInfo field) {
            var elementType = field.FieldType.GetElementType();
            const BindingFlags bindingFlags = BindingFlags.Static | BindingFlags.NonPublic;
            var method = typeof(FromComponentInParentExtensions).GetMethod(nameof(SetFieldFromComponentsInParentGeneric), bindingFlags);
            var genericMethod = method!.MakeGenericMethod(elementType);
            genericMethod.Invoke(null, new object[] { instance, field });
        }

        private static void SetFieldFromComponentsInParentGeneric<TComponent>(Component instance, FieldInfo field) {
            var components = instance.GetComponentsInParent<TComponent>(true);
            instance.SetComponentValue(field, components);
        }
    }
}