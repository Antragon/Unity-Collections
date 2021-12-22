namespace Collections.Localization {
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using Components;
    using UnityEngine;

    public class LocalizationRepository : SingletonComponent<LocalizationRepository> {
        private Dictionary<string, Dictionary<string, Dictionary<string, string>>> _texts;

        [SerializeField] private TextAsset[] _textAssets;

        protected override void AwakeExtended() {
            _texts = _textAssets.ToDictionary(x => x.name, ReadTextAsset);
        }

        private static Dictionary<string, Dictionary<string, string>> ReadTextAsset(TextAsset textAsset) {
            var text = textAsset.text.Replace("\r\n", "@");
            var textTable = CsvReader.ReadAsTable(text, '@', '|');

            var textAssetAsDictionary = textTable.Rows
                .Cast<DataRow>()
                .ToDictionary(
                    row => (string)row["key"],
                    row => Languages.All
                        .Select(language => new Tuple<string, string>(language, (string)row[language]))
                        .Where(tuple => !string.IsNullOrEmpty(tuple.Item2))
                        .ToDictionary(tuple => tuple.Item1, tuple => tuple.Item2.Replace("\\n", "\n")),
                    StringComparer.InvariantCultureIgnoreCase);
            return textAssetAsDictionary;
        }

        public bool TryGetLocalizedValue(ValueLocalization valueLocalization, out string localizedValue)
            => TryGetLocalizedValue(valueLocalization, out localizedValue, out _);

        public bool TryGetLocalizedValue(ValueLocalization valueLocalization, out string localizedValue, out string message) {
            localizedValue = string.Empty;
            message = string.Empty;

            var path = valueLocalization.Path;
            var valueKey = valueLocalization.ValueKey;
            if (!_texts.TryGetValue(path, out var values)) {
                message = $"Path {path} not found";
                return false;
            }

            if (!values.TryGetValue(valueKey, out var localizedValues)) {
                message = $"Value {path}.{valueKey} not found";
                return false;
            }

            var language = Languages.GetCurrentLanguage();
            if (!localizedValues.TryGetValue(language, out localizedValue)) {
                message = $"{language} localization not found for {path}.{valueKey}";
                return false;
            }

            return true;
        }
    }
}