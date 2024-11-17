namespace Collections.Editor.Localization {
    using Collections.Localization;
    using Collections.Localization.LocalizableComponents;
    using TMPro;
    using UnityEditor;
    using UnityEngine;

    [CustomEditor(typeof(TextProLocalization))]
    public class TextProLocalizationEditor : Editor {
        private TMP_Text _text = null!;
        private TextProLocalization _textProLocalization = null!;

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