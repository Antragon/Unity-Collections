namespace Collections {
    using System;
    using System.Collections.Generic;
    using UnityEngine;

    public static class OptionsRepository {
        private static readonly string _savePath = $"{Application.persistentDataPath}/options.json";
        private static readonly Dictionary<string, object> _options = new();

        static OptionsRepository() {
            if (DataSaver.TryLoadJson<Dictionary<string, object>>(_savePath, out var options)) {
                _options = options;
            }
        }

        public static void Set(string optionName, object option) {
            _options[optionName] = option;
            DataSaver.SaveJson(_savePath, _options);
        }

        public static bool TryGet<T>(string optionName, out T? option) {
            option = default;
            if (!_options.TryGetValue(optionName, out var value)) return false;
            if (!TryCast(value, out T? cast)) return false;
            option = cast;
            return true;
        }

        private static bool TryCast<T>(object value, out T? option) {
            try {
                option = (T)value;
                return true;
            } catch (InvalidCastException) {
                option = default;
                switch (value) {
                    case long longValue when TryCast((int)longValue, out option):
                    case double doubleValue when TryCast((float)doubleValue, out option):
                        return true;
                }

                return false;
            }
        }
    }
}