namespace Collections.Localization {
    using System;
    using UnityEngine;

    [Serializable]
    public class LocalizableString : ILocalizableValue, IEquatable<LocalizableString> {
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

        public bool Equals(LocalizableString other) {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Path == other.Path && ValueKey == other.ValueKey;
        }

        public override bool Equals(object obj) {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((LocalizableString)obj);
        }

        public override int GetHashCode() {
            throw new NotSupportedException($"Mutable class that should not implement {nameof(GetHashCode)}");
        }
    }
}