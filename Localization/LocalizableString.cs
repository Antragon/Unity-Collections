namespace Collections.Localization {
    using System;
    using UnityEngine;

    [Serializable]
    public class LocalizableString : ILocalizableValue {
        public LocalizableString(string path, string valueKey) {
            Path = path;
            ValueKey = valueKey;
        }

        [field: SerializeField] public string Path { get; set; }
        [field: SerializeField] public string ValueKey { get; set; }

        public string GetLocalizedValue(LocalizationRepository localizationRepository) {
            if (!localizationRepository.TryGetLocalizedValue(new ValueLocalization(Path, ValueKey), out var localizedValue, out var message)) {
                Debug.LogWarning(message);
                return DefaultValue();
            }

            return localizedValue;
        }

        private string DefaultValue() => $"{Path}.{ValueKey}";
    }
}