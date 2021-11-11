namespace Collections.Localization {
    using System;
    using Enums;

    [Serializable]
    public readonly struct ValueLocalization {
        public LocalizationPath Path { get; }
        public string ValueKey { get; }

        public ValueLocalization(LocalizationPath path, string valueKey) {
            Path = path;
            ValueKey = valueKey;
        }
    }
}