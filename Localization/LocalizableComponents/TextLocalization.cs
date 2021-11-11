namespace Collections.Localization.LocalizableComponents {
    using UnityEngine;
    using UnityEngine.UI;

    public class TextLocalization : LocalizableComponent {
        [SerializeField] private LocalizableString localizableString;

        public ILocalizableValue LocalizableValue => _localizableValue ??= localizableString;

        private ILocalizableValue _localizableValue;
        private Text _text;

        protected override void OnLocalizationChanged() {
            if (!_text) {
                _text = GetComponentInChildren<Text>();
            }

            _text.text = LocalizableValue.GetLocalizedValue();
        }
    }
}