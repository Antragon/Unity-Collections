namespace Collections.Localization {
    using System;
    using Enums;

    [Serializable]
    public readonly struct ValueLocalization {
        public string Path { get; }
        public string ValueKey { get; }

        public ValueLocalization(string path, string valueKey) {
            Path = path;
            ValueKey = valueKey;
        }
    }
}