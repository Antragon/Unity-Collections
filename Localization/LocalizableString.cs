namespace Collections.Localization {
    using System;
    using UnityEngine;

    [Serializable]
    public class LocalizableString : ILocalizableValue {
        public LocalizationPath path;
        public string valueKey;

        public string ValueKey => valueKey;

        public string GetLocalizedValue() {
            if (!LocalizationRepository.Self.TryGetLocalizedValue(new ValueLocalization(path, valueKey), out var localizedValue, out var message)) {
                Debug.LogWarning(message);
                return DefaultValue();
            }

            return localizedValue;
        }

        private string DefaultValue() => $"{path}.{valueKey}";
    }
}