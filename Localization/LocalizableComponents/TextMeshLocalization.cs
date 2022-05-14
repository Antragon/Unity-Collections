namespace Collections.Localization.LocalizableComponents {
    using UnityEngine;

    public class TextMeshLocalization : LocalizableComponent {
        [SerializeField] private LocalizableString _localizableString;

        private TextMesh _textMesh;

        private ILocalizableValue _localizableValue;

        public ILocalizableValue LocalizableValue {
            get => _localizableValue ??= _localizableString;
            set => SetLocalizableValue(value);
        }

        private void SetLocalizableValue(ILocalizableValue value) {
            _localizableValue = value;
            OnLocalizationChanged();
        }

        protected override void OnLocalizationChanged() {
            if (!_textMesh) {
                _textMesh = GetComponentInChildren<TextMesh>();
            }

            _textMesh.text = LocalizableValue.GetLocalizedValue();
        }
    }
}