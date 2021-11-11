namespace Collections.Localization {
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using UnityEngine;

    public class LocalizableKeyList : ILocalizableValue {
        private readonly IEnumerable<KeyCode> _keys;
        
        public string ValueKey => throw new NotImplementedException();

        public LocalizableKeyList(IEnumerable<KeyCode> keys) {
            _keys = keys;
        }

        public string GetLocalizedValue() {
            var localizedValue = string.Join(" + ", _keys.Select(GetLocalizedKeyCode));
            return localizedValue;
        }

        private static string GetLocalizedKeyCode(KeyCode keyCode) {
            if (keyCode == default) return null;
            var keyCodeAsString = keyCode.ToString();
            var valueLocalization = new ValueLocalization(LocalizationPath.Keys, keyCodeAsString);
            var canLocalizeKeyCode = LocalizationRepository.Self.TryGetLocalizedValue(valueLocalization, out var localizedValue);
            return canLocalizeKeyCode ? localizedValue : keyCodeAsString;
        }
    }
}