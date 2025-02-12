namespace Collections.Localization {
    using System;

    public class LocalizableCallbackString : ILocalizableValue {
        private readonly string _path;
        private readonly Func<string> _getValueKey;

        public LocalizableCallbackString(string path, Func<string> getValueKey) {
            _path = path;
            _getValueKey = getValueKey;
        }

        public bool IsLocalized(LocalizationRepository localizationRepository) {
            return localizationRepository.HasLocalizedValue(new ValueLocalization(_path, _getValueKey()));
        }

        public string GetLocalizedValue(LocalizationRepository localizationRepository) {
            return !localizationRepository.TryGetLocalizedValue(new ValueLocalization(_path, _getValueKey()), out var localizedValue)
                ? DefaultValue()
                : localizedValue;
        }

        private string DefaultValue() => $"{_path}.{_getValueKey()}";
    }
}