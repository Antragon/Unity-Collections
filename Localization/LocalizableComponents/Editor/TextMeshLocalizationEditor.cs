namespace Collections.Localization.LocalizableComponents.Editor {
    using UnityEditor;
    using UnityEngine;

    [CustomEditor(typeof(TextMeshLocalization))]
    public class TextMeshLocalizationEditor : Editor {
        private TextMesh _textMesh;
        private TextMeshLocalization _textMeshLocalization;

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