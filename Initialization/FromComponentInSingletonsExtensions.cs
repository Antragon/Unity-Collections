namespace Collections.Initialization {
    using System.Reflection;
    using Components;

    public static class FromComponentInSingletonsExtensions {
        public static void SetFieldFromComponentInSingletons(this object instance, FieldInfo field) {
            var attribute = field.GetCustomAttribute<FromComponentInSingletonsAttribute>();
            var component = string.IsNullOrEmpty(attribute.Tag)
                ? SingletonMarker.GetComponentInSingletons(field.FieldType)
                : SingletonMarker.GetComponentInSingletons(field.FieldType, attribute.Tag);
            instance.SetComponentValue(field, component);
        }
    }
}