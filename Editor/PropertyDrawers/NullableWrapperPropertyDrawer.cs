namespace Collections.Editor.PropertyDrawers {
    using Extensions;
    using Nullables;
    using UnityEditor;
    using UnityEngine;

    public abstract class NullableWrapperPropertyDrawerBase : PropertyDrawer {
        protected virtual float Height => 18;

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label) {
            var hasValueProperty = property.GetProperty(nameof(INullableWrapper<object>.HasValue));
            return hasValueProperty.boolValue ? Height : base.GetPropertyHeight(property, label);
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
            EditorGUI.BeginProperty(position, label, property);
            var valuePosition = EditorGUI.PrefixLabel(position, label);
            var indentLevel = EditorGUI.indentLevel;
            EditorGUI.indentLevel = 0;
            DrawNullableWrapperProperty(valuePosition, property);
            EditorGUI.indentLevel = indentLevel;
            EditorGUI.EndProperty();
        }

        private static void DrawNullableWrapperProperty(Rect position, SerializedProperty property) {
            var hasValueProperty = DrawHasValueProperty(position, property);
            if (hasValueProperty.boolValue) {
                DrawValueProperty(position, property);
            }
        }

        private static SerializedProperty DrawHasValueProperty(Rect position, SerializedProperty property) {
            var rect = position;
            rect.width = 16;
            rect.height = 16;
            var hasValueProperty = property.GetProperty(nameof(INullableWrapper<object>.HasValue));
            EditorGUI.PropertyField(rect, hasValueProperty, GUIContent.none);
            return hasValueProperty;
        }

        private static void DrawValueProperty(Rect position, SerializedProperty property) {
            var rect = position;
            rect.x += 22;
            rect.width -= 22;
            var valueProperty = property.GetProperty(nameof(INullableWrapper<object>.Value));
            EditorGUI.PropertyField(rect, valueProperty, GUIContent.none);
        }
    }

    [CustomPropertyDrawer(typeof(NullableFloatWrapper))]
    public class NullableFloatWrapperPropertyDrawer : NullableWrapperPropertyDrawerBase { }

    [CustomPropertyDrawer(typeof(NullableIntWrapper))]
    public class NullableIntWrapperPropertyDrawer : NullableWrapperPropertyDrawerBase { }

    [CustomPropertyDrawer(typeof(NullableRectWrapper))]
    public class NullableRectWrapperPropertyDrawer : NullableWrapperPropertyDrawerBase {
        protected override float Height => 38;
    }

    [CustomPropertyDrawer(typeof(NullableVector2Wrapper))]
    public class NullableVector2WrapperPropertyDrawer : NullableWrapperPropertyDrawerBase { }
}