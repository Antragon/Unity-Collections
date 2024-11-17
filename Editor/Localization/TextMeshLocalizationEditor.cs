namespace Collections.Editor.Localization {
    using Collections.Localization;
    using Collections.Localization.LocalizableComponents;
    using UnityEditor;
    using UnityEngine;

    [CustomEditor(typeof(TextMeshLocalization))]
    public class TextMeshLocalizationEditor : Editor {
        private TextMesh _textMesh = null!;
        private TextMeshLocalization _textMeshLocalization = null!;

        private void OnEnable() {
            _textMeshLocalization = (TextMeshLocalization)target;
            _textMesh = _textMeshLocalization.GetComponentInChildren<TextMesh>();
        }

        public override void OnInspectorGUI() {
            base.OnInspectorGUI();

            if (Application.isPlaying) return;
            _textMesh.text = _textMeshLocalization.LocalizableValue is LocalizableString localizableString
                ? $"{localizableString.Path}.{localizableString.ValueKey}"
                : _textMeshLocalization.LocalizableValue.ToString();

            PrefabUtility.RecordPrefabInstancePropertyModifications(_textMesh);
        }
    }
}