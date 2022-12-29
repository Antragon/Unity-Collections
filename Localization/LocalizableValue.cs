namespace Collections.Localization {
    public class LocalizableValue : ILocalizableValue {
        private readonly ILocalizableValue _value;
        private readonly ILocalizableValue[] _parameters;

        public LocalizableValue(ILocalizableValue value, params ILocalizableValue[] parameters) {
            _value = value;
            _parameters = parameters;
        }

        public string GetLocalizedValue(LocalizationRepository localizationRepository) {
            var valueString = _value.GetLocalizedValue(localizationRepository);
            if (_parameters != null) {
                for (var i = 0; i < _parameters.Length; i++) {
                    valueString = valueString.Replace($"{{{i.ToString()}}}", _parameters[i].GetLocalizedValue(localizationRepository));
                }
            }

            return valueString;
        }
    }
}