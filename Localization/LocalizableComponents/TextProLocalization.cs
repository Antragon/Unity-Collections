namespace Collections.Localization.LocalizableComponents {
    using Initialization;
    using TMPro;
    using UnityEngine;
    using UnityEngine.Serialization;

    public class TextProLocalization : LocalizableComponent, ITextLocalization {
        [FromComponentInSingletons] private readonly LocalizationRepository _localizationRepository = null!;

        [FormerlySerializedAs("localizableString")] [SerializeField] private LocalizableString _localizableString = null!;

        [FromComponentInChildren, SerializeField] private TMP_Text _text = null!;

        private ILocalizableValue? _localizableValue;

        public ILocalizableValue LocalizableValue {
            get => _localizableValue ??= _localizableString;
            set => SetLocalizableValue(value);
        }

        private void SetLocalizableValue(ILocalizableValue value) {
            _localizableValue = value;
            OnLocalizationChanged();
        }

        protected override void OnLocalizationChanged() {
            InitializeOnce();
            _text.text = LocalizableValue.GetLocalizedValue(_localizationRepository);
        }
    }
}