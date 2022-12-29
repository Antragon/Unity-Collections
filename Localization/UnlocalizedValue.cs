namespace Collections.Localization {
    public class UnlocalizedValue : ILocalizableValue {
        private readonly string _value;

        public UnlocalizedValue(string value) {
            _value = value;
        }

        public string GetLocalizedValue(LocalizationRepository localizationRepository) => _value;
    }
}