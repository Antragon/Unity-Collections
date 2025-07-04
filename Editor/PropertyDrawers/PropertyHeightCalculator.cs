namespace Collections.Editor.PropertyDrawers {
    using System.Linq;
    using Extensions;
    using UnityEditor;

    public class PropertyHeightCalculator {
        private readonly float _spacing = EditorGUIUtility.standardVerticalSpacing;
        private readonly SerializedProperty _property;

        public PropertyHeightCalculator(SerializedProperty property) {
            _property = property;
        }

        public float GetHeight(params string[] childNames) {
            return childNames.Sum(GetHeight);
        }

        private float GetHeight(string childName) {
            var child = _property.GetProperty(childName);
            return EditorGUI.GetPropertyHeight(child, true) + _spacing;
        }
    }
}