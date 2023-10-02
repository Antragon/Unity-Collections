namespace Collections.Localization {
    using System.Linq;

    public class LocalizableFallbackValue : ILocalizableValue {
        private readonly ILocalizableValue[] _values;

        private ILocalizableValue _value;

        public LocalizableFallbackValue(params ILocalizableValue[] values) {
            _values = values;
        }

        public bool IsLocalized(LocalizationRepository localizationRepository) {
            AssignLocalizedValue(localizationRepository);
            return _value.IsLocalized(localizationRepository);
        }

        public string GetLocalizedValue(LocalizationRepository localizationRepository) {
            AssignLocalizedValue(localizationRepository);
            return _value.GetLocalizedValue(localizationRepository);
        }

        private void AssignLocalizedValue(LocalizationRepository localizationRepository) {
            _value ??= _values.FirstOrDefault(v => v.IsLocalized(localizationRepository)) ?? _values.Last();
        }
    }
}