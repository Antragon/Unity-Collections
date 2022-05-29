namespace Collections.Editor.PropertyDrawers {
    using Nullables;
    using UnityEditor;
    using UnityEngine;

    public abstract class NullableWrapperPropertyDrawerBase<T> : PropertyDrawer {
        private Rect _rect;
        private SerializedProperty _property;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
            _rect = EditorGUI.PrefixLabel(position, label);
            _property = property;
            var indentLevel = EditorGUI.indentLevel;
            EditorGUI.indentLevel = 0;
            DrawNullableWrapperProperty();
            EditorGUI.indentLevel = indentLevel;
        }

        private void DrawNullableWrapperProperty() {
            var hasValueProperty = DrawHasValueProperty();
            if (hasValueProperty.boolValue) {
                DrawValueProperty();
            }
        }

        private SerializedProperty DrawHasValueProperty() {
            var rect = _rect;
            rect.width = 16;
            var hasValueProperty = GetProperty(nameof(INullableWrapper<T>.HasValue));
            EditorGUI.PropertyField(rect, hasValueProperty, GUIContent.none);
            return hasValueProperty;
        }

        private void DrawValueProperty() {
            var rect = _rect;
            rect.x += 22;
            rect.width -= 22;
            var valueProperty = GetProperty(nameof(INullableWrapper<T>.Value));
            EditorGUI.PropertyField(rect, valueProperty, GUIContent.none);
        }

        private SerializedProperty GetProperty(string childPropertyName) {
            return _property.FindPropertyRelative($"<{childPropertyName}>k__BackingField");
        }
    }

    [CustomPropertyDrawer(typeof(NullableFloatWrapper))]
    public class NullableFloatWrapperPropertyDrawer : NullableWrapperPropertyDrawerBase<float> { }
}