namespace Collections.Localization.LocalizableComponents {
    using Components;
    using Initialization;
    using UnityEngine;
    using UnityEngine.Serialization;
    using UnityEngine.UI;

    public class TextLocalization : LocalizableComponent, ITextLocalization {
        [FromComponent(SingletonTag = GameControl.Tag)] private readonly LocalizationRepository _localizationRepository;

        [FormerlySerializedAs("localizableString")] [SerializeField] private LocalizableString _localizableString;

        [FromComponentInChildren, SerializeField] private Text _text;

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
            InitializeOnce();
            _text.text = LocalizableValue.GetLocalizedValue(_localizationRepository);
        }
    }
}