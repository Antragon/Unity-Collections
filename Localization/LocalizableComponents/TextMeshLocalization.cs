namespace Collections.Localization.LocalizableComponents {
    using Initialization;
    using UnityEngine;

    public class TextMeshLocalization : LocalizableComponent, ITextLocalization {
        [FromComponentInSingletons] private readonly LocalizationRepository _localizationRepository;

        [SerializeField] private LocalizableString _localizableString;

        [FromComponentInChildren, SerializeField] private TextMesh _textMesh;

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
            _textMesh.text = LocalizableValue.GetLocalizedValue(_localizationRepository);
        }
    }
}