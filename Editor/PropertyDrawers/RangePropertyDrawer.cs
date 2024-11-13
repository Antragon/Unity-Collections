namespace Collections.Editor.PropertyDrawers {
    using System;
    using Ranges;
    using UnityEditor;
    using UnityEngine;

    public abstract class RangePropertyDrawer<T> : PropertyDrawer
    where T : IComparable {
        private const string _startName = nameof(IRange<T>.Start);
        private const string _endName = nameof(IRange<T>.End);

        private SingleLineLayout _layout;
        private SerializedProperty _property;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
            position = EditorGUI.PrefixLabel(position, new GUIContent(label));
            _layout = new SingleLineLayout(position, 4);
            _property = property;
            DrawProperty();
        }

        private void DrawProperty() {
            DrawStart();
            DrawEnd();
        }

        private void DrawStart() {
            var property = GetProperty(_startName);

            var labelRect = _layout.Get(0, 1);
            EditorGUI.LabelField(labelRect, ObjectNames.NicifyVariableName(_startName));

            var valueRect = _layout.Get(1, 1);
            EditorGUI.PropertyField(valueRect, property, GUIContent.none);
        }

        private void DrawEnd() {
            var property = GetProperty(_endName);

            var labelRect = _layout.Get(2, 1);
            EditorGUI.LabelField(labelRect, ObjectNames.NicifyVariableName(_endName));

            var valueRect = _layout.Get(3, 1);
            EditorGUI.PropertyField(valueRect, property, GUIContent.none);
        }

        private SerializedProperty GetProperty(string childPropertyName) {
            return _property.FindPropertyRelative($"<{childPropertyName}>k__BackingField");
        }
    }

    [CustomPropertyDrawer(typeof(FloatRange))]
    public class FloatRangePropertyDrawer : RangePropertyDrawer<float> { }

    [CustomPropertyDrawer(typeof(IntRange))]
    public class IntRangePropertyDrawer : RangePropertyDrawer<int> { }
}