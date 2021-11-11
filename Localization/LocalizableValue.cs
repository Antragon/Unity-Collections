namespace Collections.Localization {
    public class LocalizableValue : ILocalizableValue {
        private readonly ILocalizableValue _value;
        private readonly ILocalizableValue[] _parameters;

        public string ValueKey => _value.ValueKey;

        public LocalizableValue(ILocalizableValue value, params ILocalizableValue[] parameters) {
            _value = value;
            _parameters = parameters;
        }

        public string GetLocalizedValue() {
            var valueString = _value.GetLocalizedValue();
            if (_parameters != null) {
                for (var i = 0; i < _parameters.Length; i++) {
                    valueString = valueString.Replace($"{{{i.ToString()}}}", _parameters[i].GetLocalizedValue());
                }
            }

            return valueString;
        }
    }
}