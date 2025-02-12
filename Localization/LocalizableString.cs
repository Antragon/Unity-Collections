namespace Collections.Localization {
    using System;
    using System.Diagnostics.CodeAnalysis;
    using UnityEngine;

    [Serializable]
    public class LocalizableString : ILocalizableValue, IEquatable<LocalizableString> {
        public LocalizableString(string path, string valueKey) {
            Path = path;
            ValueKey = valueKey;
        }

        [field: SerializeField] public string Path { get; private set; }
        [field: SerializeField] public string ValueKey { get; private set; }

        public bool IsLocalized(LocalizationRepository localizationRepository) {
            return localizationRepository.HasLocalizedValue(new ValueLocalization(Path, ValueKey));
        }

        public string GetLocalizedValue(LocalizationRepository localizationRepository) {
            return !localizationRepository.TryGetLocalizedValue(new ValueLocalization(Path, ValueKey), out var localizedValue)
                ? DefaultValue()
                : localizedValue;
        }

        private string DefaultValue() => $"{Path}.{ValueKey}";

        public bool Equals(LocalizableString? other) {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;
            return Path == other.Path && ValueKey == other.ValueKey;
        }

        public override bool Equals(object? obj) {
            if (obj is null) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((LocalizableString)obj);
        }

        [SuppressMessage("ReSharper", "NonReadonlyMemberInGetHashCode", Justification = "TODO: Convert to struct, maybe...")]
        public override int GetHashCode() {
            return HashCode.Combine(Path, ValueKey);
        }
    }
}