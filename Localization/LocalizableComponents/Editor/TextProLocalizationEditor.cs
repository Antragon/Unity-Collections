namespace Collections.Localization.LocalizableComponents.Editor {
    using TMPro;
    using UnityEditor;
    using UnityEngine;

    [CustomEditor(typeof(TextProLocalization))]
    public class TextProLocalizationEditor : Editor {
        private TMP_Text _text;
        private TextProLocalization _textProLocalization;

        private void OnEnable() {
            _textProLocalization = (TextProLocalization)target;
            _text = _textProLocalization.GetComponentInChildren<TMP_Text>();
        }

        public override void OnInspectorGUI() {
            base.OnInspectorGUI();

            if (Application.isPlaying) return;
            if (_textProLocalization.LocalizableValue is LocalizableString localizableString) {
                _text.text = $"{localizableString.Path}.{localizableString.ValueKey}";
                PrefabUtility.RecordPrefabInstancePropertyModifications(_text);
            }
        }
    }
}