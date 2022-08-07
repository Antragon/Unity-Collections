namespace Collections.Localization.LocalizableComponents {
    using UnityEngine;
    using UnityEngine.Serialization;
    using UnityEngine.UI;

    public class TextLocalization : LocalizableComponent, ITextLocalization {
        [FormerlySerializedAs("localizableString")] [SerializeField] private LocalizableString _localizableString;

        private Text _text;

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
                _text = GetComponentInChildren<Text>();
            }

            _text.text = LocalizableValue.GetLocalizedValue();
        }
    }
}