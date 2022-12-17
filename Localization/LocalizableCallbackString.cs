namespace Collections.Localization {
    using System;
    using UnityEngine;

    public class LocalizableCallbackString : ILocalizableValue {
        private readonly string _path;
        private readonly Func<string> _getValueKey;

        public LocalizableCallbackString(string path, Func<string> getValueKey) {
            _path = path;
            _getValueKey = getValueKey;
        }

        public string GetLocalizedValue() {
            if (!LocalizationRepository.Self.TryGetLocalizedValue(new ValueLocalization(_path, _getValueKey()), out var localizedValue, out var message)) {
                Debug.LogWarning(message);
                return DefaultValue();
            }

            return localizedValue;
        }

        private string DefaultValue() => $"{_path}.{_getValueKey()}";
    }
}