namespace Collections.Localization {
    using System.Linq;
    using Components;
    using JetBrains.Annotations;
    using Observable;
    using UnityEngine;

    public class LocalizationManager : SingletonComponent<LocalizationManager> {
        private static string LocalizationSavePath => $"{Application.persistentDataPath}/language.json";

        private readonly ObservableAction _onLocalizationChanged = new ObservableAction();

        [SerializeField] private SystemLanguage _defaultLanguage;

        [field: SerializeField] public SystemLanguage[] Languages { get; [UsedImplicitly] private set; }

        public Observable OnLocalizationChanged => _onLocalizationChanged.ToObservable();

        private SystemLanguage? _currentLanguage;

        public SystemLanguage CurrentLanguage {
            get => _currentLanguage ??= GetLanguage();
            set => SetLanguage(value);
        }

        private SystemLanguage GetLanguage() {
            lock (LocalizationSavePath) {
                if (DataSaver.TryLoadJson<SystemLanguage>(LocalizationSavePath, out var language) && Languages.Contains(language)) {
                    return language;
                }

                language = Languages.Contains(Application.systemLanguage) ? Application.systemLanguage : _defaultLanguage;
                DataSaver.SaveJson(language, LocalizationSavePath);
                return language;
            }
        }

        private void SetLanguage(SystemLanguage value) {
            lock (LocalizationSavePath) {
                _currentLanguage = value;
                DataSaver.SaveJson(value, LocalizationSavePath);
                _onLocalizationChanged.Invoke();
            }
        }
    }
}