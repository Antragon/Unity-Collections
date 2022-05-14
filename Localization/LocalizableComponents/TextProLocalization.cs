namespace Collections.Localization.LocalizableComponents {
    using TMPro;
    using UnityEngine;
    using UnityEngine.Serialization;

    public class TextProLocalization : LocalizableComponent {
        [FormerlySerializedAs("localizableString")] [SerializeField] private LocalizableString _localizableString;

        [SerializeField] private TMP_Text _text;

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
            if (!_text) {
                _text = GetComponentInChildren<TMP_Text>();
            }

            _text.text = LocalizableValue.GetLocalizedValue();
        }
    }
}