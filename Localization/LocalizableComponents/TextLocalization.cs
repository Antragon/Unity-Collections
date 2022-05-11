namespace Collections.Localization.LocalizableComponents {
    using UnityEngine;
    using UnityEngine.Serialization;
    using UnityEngine.UI;

    public class TextLocalization : LocalizableComponent {
        [FormerlySerializedAs("localizableString")] [SerializeField] private LocalizableString _localizableString;

        private Text _text;

        private ILocalizableValue _localizableValue;

        public ILocalizableValue LocalizableValue => _localizableValue ??= _localizableString;

        protected override void OnLocalizationChanged() {
            if (!_text) {
                _text = GetComponentInChildren<Text>();
            }

            _text.text = LocalizableValue.GetLocalizedValue();
        }
    }
}