namespace Collections.Initialization {
    using System.Reflection;
    using Components;
    using Extensions;

    public static class FromComponentInSingletonsExtensions {
        public static void SetFieldFromComponentInSingletons(this object instance, FieldInfo field) {
            var attribute = field.GetCustomAttribute<FromComponentInSingletonsAttribute>();
            var component = attribute.Tag.HasValue()
                ? SingletonMarker.GetComponentInSingletons(field.FieldType, attribute.Tag!)
                : SingletonMarker.GetComponentInSingletons(field.FieldType);
            instance.SetComponentValue(field, component);
        }
    }
}