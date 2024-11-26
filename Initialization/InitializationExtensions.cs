namespace Collections.Initialization {
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using JetBrains.Annotations;
    using MoreLinq;
    using UnityEngine;
    using Object = UnityEngine.Object;

#line hidden
    public static class InitializationExtensions {
        private static readonly HashSet<Type> _unityTypes = new() {
            typeof(MonoBehaviour),
            typeof(ScriptableObject)
        };

        public static void Initialize(this Component instance) {
            instance.Initialize(instance);
        }

        public static void Initialize<T>(this T instance, Component componentProvider)
        where T : class {
            var type = instance.GetType();
            instance.Initialize(componentProvider, type);
        }

        private static void Initialize(this object instance, Component componentProvider, Type? type) {
            while (type != null && !_unityTypes.Contains(type)) {
                var fields = type.GetFields(BindingFlags.Instance | BindingFlags.NonPublic);
                fields.Where(HasAttribute<FromComponentAttribute>).ForEach(f => instance.SetFieldFromComponent(f, componentProvider));
                fields.Where(HasAttribute<FromComponentsAttribute>).ForEach(f => instance.SetFieldFromComponents(f, componentProvider));
                fields.Where(HasAttribute<FromComponentInParentAttribute>).ForEach(f => instance.SetFieldFromComponentInParent(f, componentProvider));
                fields.Where(HasAttribute<FromComponentsInParentAttribute>).ForEach(f => instance.SetFieldFromComponentsInParent(f, componentProvider));
                fields.Where(HasAttribute<FromComponentInChildrenAttribute>).ForEach(f => instance.SetFieldFromComponentInChildren(f, componentProvider));
                fields.Where(HasAttribute<FromComponentsInChildrenAttribute>).ForEach(f => instance.SetFieldFromComponentsInChildren(f, componentProvider));
                fields.Where(HasAttribute<FromComponentInSingletonsAttribute>).ForEach(instance.SetFieldFromComponentInSingletons);
                fields.Where(MustNotBeNull).ForEach(instance.ValidateNotNull);
                type = type.BaseType;
            }
        }

        public static void Initialize<T>(this T instance)
        where T : ScriptableObject {
            if (!Application.isPlaying) return;
            var type = typeof(T);
            while (type != null && _unityTypes.Contains(type)) {
                var fields = type.GetFields(BindingFlags.Instance | BindingFlags.NonPublic);
                fields.Where(MustNotBeNull).ForEach(instance.ValidateNotNull);
                type = type.BaseType;
            }
        }

        private static bool MustNotBeNull(FieldInfo fieldInfo) {
            var validatable = fieldInfo.HasAttribute<SerializeField>()
                              || fieldInfo.HasAttribute<FromComponentAttribute>()
                              || fieldInfo.HasAttribute<FromComponentInParentAttribute>()
                              || fieldInfo.HasAttribute<FromComponentInChildrenAttribute>()
                              || fieldInfo.HasAttribute<FromComponentInSingletonsAttribute>();
            return validatable && !IsNullable(fieldInfo);
        }

        private static bool IsNullable(FieldInfo fieldInfo) {
            return fieldInfo
                       .GetCustomAttributesData()
                       .Any(a => a.AttributeType.FullName == "System.Runtime.CompilerServices.NullableAttribute")
                   || fieldInfo.HasAttribute<CanBeNullAttribute>()
                   || fieldInfo.HasAttribute<OptionalAttribute>();
        }

        private static void ValidateNotNull(this object instance, FieldInfo field) {
            var value = field.GetValue(instance);
            if (value is Object @object && !@object ||
                value is string @string && string.IsNullOrEmpty(@string) ||
                value == null) {
                Debug.LogError($"{field.Name} must not be null");
            }
        }

        private static bool HasAttribute<T>(this MemberInfo memberInfo) {
            return memberInfo.HasAttribute(typeof(T));
        }

        private static bool HasAttribute(this MemberInfo memberInfo, Type attributeType) {
            return Attribute.IsDefined(memberInfo, attributeType);
        }
    }
#line default
}