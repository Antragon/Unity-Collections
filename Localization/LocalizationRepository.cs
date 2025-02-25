﻿namespace Collections.Localization {
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using Extensions;
    using Initialization;
    using UnityEngine;

    [DefaultExecutionOrder(DefaultExecutionOrders.Early)]
    public class LocalizationRepository : MonoBehaviour {
        private Dictionary<string, Dictionary<string, Dictionary<string, string>>> _texts = null!;

        [FromComponent] private readonly LocalizationManager _localizationManager = null!;

        [SerializeField] private bool _ignoreEmpty;
        [SerializeField] private TextAsset[] _textAssets = null!;

        private void Awake() {
            this.Initialize();
            _texts = _textAssets.ToDictionary(x => x.name, ReadTextAsset);
        }

        private Dictionary<string, Dictionary<string, string>> ReadTextAsset(TextAsset textAsset) {
            var text = textAsset.text.Replace("\r\n", "@");
            var textTable = CsvReader.ReadAsTable(text, '@', '|');

            var textAssetAsDictionary = textTable.Rows
                .Cast<DataRow>()
                .ToDictionary(
                    row => (string)row["key"],
                    row => _localizationManager.Languages
                        .Select(language => language.ToString())
                        .Select(language => new Tuple<string, string>(language, (string)row[language]))
                        .Where(tuple => !string.IsNullOrEmpty(tuple.Item2))
                        .ToDictionary(tuple => tuple.Item1, tuple => tuple.Item2.Replace("\\n", "\n")));
            return textAssetAsDictionary;
        }

        public IEnumerable<string> GetKeys(string path) {
            return _texts[path].Keys.AsEnumerable();
        }

        public bool HasLocalizedValue(ValueLocalization valueLocalization) {
            var path = valueLocalization.Path;
            var valueKey = valueLocalization.ValueKey;
            return _texts.TryGetValue(path, out var values) && values.ContainsKey(valueKey);
        }

        public bool TryGetLocalizedValue(ValueLocalization valueLocalization, out string localizedValue) {
            localizedValue = string.Empty;

            var path = valueLocalization.Path;
            var valueKey = valueLocalization.ValueKey;
            if (_ignoreEmpty) {
                if (!path.HasValue() || !valueKey.HasValue()) {
                    localizedValue = string.Empty;
                    return true;
                }
            }
            
            if (!_texts.TryGetValue(path, out var values)) {
                Debug.LogWarning($"Path {path} not found");
                return false;
            }

            if (!values.TryGetValue(valueKey, out var localizedValues)) {
                Debug.LogWarning($"Value {path}.{valueKey} not found");
                return false;
            }

            var language = _localizationManager.Language.ToString();
            if (!localizedValues.TryGetValue(language, out localizedValue)) {
                Debug.LogWarning($"{language} localization not found for {path}.{valueKey}");
                return false;
            }

            return true;
        }
    }
}