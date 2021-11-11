namespace Collections.Localization {
    public class UnlocalizedValue : ILocalizableValue {
        public string ValueKey { get; }

        public UnlocalizedValue(string value) {
            ValueKey = value;
        }

        public string GetLocalizedValue() => ValueKey;
    }
}