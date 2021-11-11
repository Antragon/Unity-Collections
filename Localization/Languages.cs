namespace Collections.Localization {
    using System.Collections.Generic;
    using UnityEngine;

    public static class Languages {
        private const string _english = "English";
        private const string _deutsch = "Deutsch";

        private static string Default => _english;

        public static List<string> All { get; } = new List<string> {
            _english,
            _deutsch,
        };

        public static string GetCurrentLanguage() {
            var language = PlayerPrefs.GetString("language");
            if (All.Contains(language)) return language;

            if (!SystemLanguageMapping.TryGetValue(Application.systemLanguage, out var mappedSystemLanguage)) {
                mappedSystemLanguage = Default;
            }

            SaveLanguage(mappedSystemLanguage);
            return mappedSystemLanguage;
        }

        private static Dictionary<SystemLanguage, string> SystemLanguageMapping { get; } = new Dictionary<SystemLanguage, string> {
            [SystemLanguage.English] = _english,
            [SystemLanguage.German] = _deutsch,
        };

        public static void SaveLanguage(string language) {
            PlayerPrefs.SetString("language", language);
        }
    }
}