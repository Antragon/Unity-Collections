namespace Collections.Initialization {
    using System;
    using System.Linq;
    using System.Reflection;
    using Components;
    using MoreLinq;
    using UnityEngine;
    using Object = UnityEngine.Object;

#line hidden
    public static class InitializationExtensions {
        private static readonly Type _monoBehaviourType = typeof(MonoBehaviour);

        public static void Initialize(this Component instance) {
            var type = instance.GetType();
            instance.Initialize(type);
        }

        private static void Initialize(this Component instance, Type type) {
            while (type != null && type != _monoBehaviourType && _monoBehaviourType.IsAssignableFrom(type)) {
                var fields = type.GetFields(BindingFlags.Instance | BindingFlags.NonPublic);
                fields.Where(HasAttribute<FromComponentAttribute>).ForEach(instance.SetFieldFromComponent);
                fields.Where(HasAttribute<FromComponentsAttribute>).ForEach(instance.SetFieldFromComponents);
                fields.Where(HasAttribute<FromComponentInParentAttribute>).ForEach(instance.SetFieldFromComponentInParent);
                fields.Where(HasAttribute<FromComponentsInParentAttribute>).ForEach(instance.SetFieldFromComponentsInParent);
                fields.Where(HasAttribute<FromComponentInChildrenAttribute>).ForEach(instance.SetFieldFromComponentInChildren);
                fields.Where(HasAttribute<FromComponentsInChildrenAttribute>).ForEach(instance.SetFieldFromComponentsInChildren);
                fields.Where(MustNotBeNull).ForEach(instance.ValidateNotNull);
                type = type.BaseType;
            }
        }

        private static bool MustNotBeNull(FieldInfo fieldInfo) {
            var validatable = fieldInfo.HasAttribute<SerializeField>() ||
                              fieldInfo.HasAttribute<FromComponentAttribute>() ||
                              fieldInfo.HasAttribute<FromComponentInParentAttribute>() ||
                              fieldInfo.HasAttribute<FromComponentInChildrenAttribute>();
            return validatable && !fieldInfo.HasAttribute<OptionalAttribute>();
        }

        private static void ValidateNotNull(this object instance, FieldInfo field) {
            var value = field.GetValue(instance);
            if (value is Object @object && !@object ||
                value is string @string && string.IsNullOrEmpty(@string) ||
                value == null) {
                InfoLogger.Self.WriteError($"{field.Name} must not be null");
            }
        }

        private static bool HasAttribute<T>(this MemberInfo memberInfo) {
            return Attribute.IsDefined(memberInfo, typeof(T));
        }
    }
#line default
}