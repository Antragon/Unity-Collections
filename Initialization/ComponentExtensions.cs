namespace Collections.Initialization {
    using System.Reflection;
    using UnityEngine;

    public static class ComponentExtensions {
        public static void SetComponentValue(this object instance, FieldInfo field, object? value) {
            if ((Component)field.GetValue(instance)) return;
            field.SetValue(instance, value);
        }

        public static void SetComponentValue<TComponent>(this object instance, FieldInfo field, TComponent[] values) {
            if (field.GetValue(instance) is TComponent[] { Length: > 0 }) return;
            field.SetValue(instance, values);
        }
    }
}