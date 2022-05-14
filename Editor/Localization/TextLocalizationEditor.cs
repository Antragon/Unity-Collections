namespace Collections.Editor.Localization {
    using Collections.Localization;
    using Collections.Localization.LocalizableComponents;
    using UnityEditor;
    using UnityEngine;
    using UnityEngine.UI;

    [CustomEditor(typeof(TextLocalization))]
    public class TextLocalizationEditor : Editor {
        private Text _text;
        private TextLocalization _textLocalization;

        private void OnEnable() {
            _textLocalization = (TextLocalization)target;
            _text = _textLocalization.GetComponentInChildren<Text>();
        }

        public override void OnInspectorGUI() {
            base.OnInspectorGUI();

            if (Application.isPlaying) return;
            if (_textLocalization.LocalizableValue is LocalizableString localizableString) {
                _text.text = $"{localizableString.Path}.{localizableString.ValueKey}";
                PrefabUtility.RecordPrefabInstancePropertyModifications(_text);
            }
        }
    }
}