namespace Collections.Localization.LocalizableComponents {
    using TMPro;
    using UnityEngine;
    using UnityEngine.Serialization;

    public class TextProLocalization : LocalizableComponent {
        [FormerlySerializedAs("localizableString")] [SerializeField] private LocalizableString _localizableString;

        private ILocalizableValue _localizableValue;
        private TMP_Text _text;

        public ILocalizableValue LocalizableValue {
            get => _localizableValue ??= _localizableString;
            set {
                _localizableValue = value;
                OnLocalizationChanged();
            }
        }

        protected override void OnLocalizationChanged() {
            if (!_text) {
                _text = GetComponent<TMP_Text>();
            }

            _text.text = LocalizableValue.GetLocalizedValue();
        }
    }
}