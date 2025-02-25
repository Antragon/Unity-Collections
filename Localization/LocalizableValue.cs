﻿namespace Collections.Localization {
    using System.Linq;

    public class LocalizableValue : ILocalizableValue {
        private readonly ILocalizableValue _value;
        private readonly ILocalizableValue[] _parameters;

        public LocalizableValue(ILocalizableValue value, params ILocalizableValue[] parameters) {
            _value = value;
            _parameters = parameters;
        }

        public bool IsLocalized(LocalizationRepository localizationRepository) {
            return _value.IsLocalized(localizationRepository) && _parameters.All(p => p.IsLocalized(localizationRepository));
        }

        public string GetLocalizedValue(LocalizationRepository localizationRepository) {
            var valueString = _value.GetLocalizedValue(localizationRepository);
            for (var i = 0; i < _parameters.Length; i++) {
                valueString = valueString.Replace($"{{{i.ToString()}}}", _parameters[i].GetLocalizedValue(localizationRepository));
            }

            return valueString;
        }
    }
}