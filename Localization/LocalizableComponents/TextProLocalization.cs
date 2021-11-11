namespace Collections.Localization.LocalizableComponents {
    using TMPro;
    using UnityEngine;

    [RequireComponent(typeof(TextMeshProUGUI))]
    public class TextProLocalization : LocalizableComponent {
        [SerializeField] private LocalizableString localizableString;

        public ILocalizableValue LocalizableValue {
            get => _localizableValue ??= localizableString;
            set {
                _localizableValue = value;
                OnLocalizationChanged();
            }
        }

        private ILocalizableValue _localizableValue;
        private TextMeshProUGUI _text;

        protected override void OnLocalizationChanged() {
            if (!_text) {
                _text = GetComponent<TextMeshProUGUI>();
            }

            _text.text = LocalizableValue.GetLocalizedValue();
        }
    }
}