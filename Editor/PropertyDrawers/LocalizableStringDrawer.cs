namespace Collections.Editor.PropertyDrawers {
    using Collections.Localization;
    using UnityEditor;
    using UnityEngine;

    [CustomPropertyDrawer(typeof(LocalizableString))]
    public class LocalizableStringDrawer : PropertyDrawer {
        private const string _pathName = nameof(LocalizableString.Path);
        private const string _valueKeyName = nameof(LocalizableString.ValueKey);

        private Rect _rect;
        private SerializedProperty _property;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
            _rect = EditorGUI.PrefixLabel(position, label);
            _property = property;
            var indentLevel = EditorGUI.indentLevel;
            EditorGUI.indentLevel = 0;
            DrawProperty();
            EditorGUI.indentLevel = indentLevel;
        }

        private void DrawProperty() {
            DrawPath();
            DrawSeparator();
            DrawValueKey();
        }

        private void DrawPath() {
            var rect = _rect;
            var property = GetProperty(_pathName);
            var guiStyle = new GUIStyle(GUI.skin.textField);
            guiStyle.CalcMinMaxWidth(new GUIContent(property.stringValue), out var min, out _);
            rect.width = Mathf.Max(min, 16);
            property.stringValue = EditorGUI.TextField(rect, property.stringValue);
            _rect.x += rect.width;
            _rect.width -= rect.width;
        }

        private void DrawSeparator() {
            var rect = _rect;
            rect.width = 6;
            EditorGUI.LabelField(rect, ".");
            _rect.x += rect.width;
            _rect.width -= rect.width;
        }

        private void DrawValueKey() {
            var rect = _rect;
            var property = GetProperty(_valueKeyName);
            property.stringValue = EditorGUI.TextField(rect, property.stringValue);
        }

        private SerializedProperty GetProperty(string childPropertyName) {
            return _property.FindPropertyRelative($"<{childPropertyName}>k__BackingField");
        }
    }
}