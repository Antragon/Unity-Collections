﻿namespace Collections.Initialization {
    using System.Linq;
    using System.Reflection;
    using UnityEngine;

    public static class ComponentExtensions {
        public static void SetComponentValue(this Component instance, FieldInfo field, Component value) {
            if ((Component)field.GetValue(instance)) return;
            field.SetValue(instance, value);
        }

        public static void SetComponentValue<TComponent>(this Component instance, FieldInfo field, TComponent[] values) {
            if (field.GetValue(instance) is TComponent[] existing && existing.Any()) return;
            field.SetValue(instance, values);
        }
    }
}