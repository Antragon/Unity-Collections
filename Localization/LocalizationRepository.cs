namespace Collections.Localization {
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using Components;
    using Enums;
    using UnityEngine;

    public class LocalizationRepository : SingletonComponent<LocalizationRepository> {
        [SerializeField] private TextAsset _guiTexts;
        [SerializeField] private TextAsset _hotKeys;
        [SerializeField] private TextAsset _keys;
        [SerializeField] private TextAsset _tooltips;

        private readonly Dictionary<LocalizationPath, Dictionary<string, Dictionary<string, string>>> _texts =
            new Dictionary<LocalizationPath, Dictionary<string, Dictionary<string, string>>>();

        protected override void AwakeExtended() {
            _texts.Add(LocalizationPath.Gui, ReadTextAsset(_guiTexts));
            _texts.Add(LocalizationPath.Hotkeys, ReadTextAsset(_hotKeys));
            _texts.Add(LocalizationPath.Keys, ReadTextAsset(_keys));
            _texts.Add(LocalizationPath.Tooltips, ReadTextAsset(_tooltips));
        }

        private static Dictionary<string, Dictionary<string, string>> ReadTextAsset(TextAsset textAsset) {
            var text = textAsset.text.Replace("\r\n", "@");
            var textTable = CsvReader.ReadAsTable(text, '@', '|');

            var textAssetAsDictionary = textTable.Rows
                .Cast<DataRow>()
                .ToDictionary(
                    row => (string) row["key"],
                    row => Languages.All
                        .Select(language => new Tuple<string, string>(language, (string) row[language]))
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