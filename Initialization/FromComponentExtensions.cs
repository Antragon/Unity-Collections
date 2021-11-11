﻿namespace Collections.Initialization {
    using System.Reflection;
    using UnityEngine;

    public static class FromComponentExtensions {
        public static void SetFieldFromComponent(this Component instance, FieldInfo field) {
            var component = instance.GetComponent(field.FieldType);
            field.SetValue(instance, component);
        }

        public static void SetFieldFromComponents(this Component instance, FieldInfo field) {
            var elementType = field.FieldType.GetElementType();
            const BindingFlags bindingFlags = BindingFlags.Static | BindingFlags.NonPublic;
            var method = typeof(FromComponentExtensions).GetMethod(nameof(SetFieldFromComponentsGeneric), bindingFlags);
            var genericMethod = method!.MakeGenericMethod(elementType);
            genericMethod.Invoke(null, new object[] { instance, field });
        }

        private static void SetFieldFromComponentsGeneric<TComponent>(Component instance, FieldInfo field) {
            var components = instance.GetComponents<TComponent>();
            field.SetValue(instance, components);
        }
    }
}