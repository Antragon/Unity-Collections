namespace Collections.Localization {
    using System;
    using UnityEngine;

    public static class SystemLanguageExtensions {
        public static string ToLocalizedString(this SystemLanguage systemLanguage) {
            var localizedLanguage = systemLanguage switch {
                SystemLanguage.English => "English",
                SystemLanguage.German => "Deutsch",
                _ => throw new ArgumentOutOfRangeException(nameof(systemLanguage), systemLanguage, null)
            };
            return localizedLanguage;
        }
    }
}