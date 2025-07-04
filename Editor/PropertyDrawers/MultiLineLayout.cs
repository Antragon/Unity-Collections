namespace Collections.Editor.PropertyDrawers {
    using Extensions;
    using UnityEditor;
    using UnityEngine;

    public class MultiLineLayout {
        private readonly float _spacing = EditorGUIUtility.standardVerticalSpacing;
        private readonly SerializedProperty _property;

        private Rect _position;

        public MultiLineLayout(SerializedProperty property, Rect position) {
            _property = property;
            _position = position;
        }

        public void Draw(string childName, bool enabled = true) {
            var child = _property.GetProperty(childName);
            if (!enabled) {
                EditorGUI.BeginDisabledGroup(true);
            }

            EditorGUI.PropertyField(_position, child, true);
            EditorGUI.EndDisabledGroup();
            _position.y += EditorGUI.GetPropertyHeight(child, true) + _spacing;
        }
    }
}