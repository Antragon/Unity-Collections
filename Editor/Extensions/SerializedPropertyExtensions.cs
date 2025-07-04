namespace Collections.Editor.Extensions {
    using UnityEditor;

    public static class SerializedPropertyExtensions {
        public static SerializedProperty GetProperty(this SerializedProperty property, string childPropertyName) {
            return property.FindPropertyRelative($"<{childPropertyName}>k__BackingField");
        }
    }
}