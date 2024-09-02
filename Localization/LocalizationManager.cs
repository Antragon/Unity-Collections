namespace Collections.Localization {
    using System.Linq;
    using JetBrains.Annotations;
    using Observable;
    using UnityEngine;

    public class LocalizationManager : MonoBehaviour {
        private readonly ObservableAction _onLocalizationChanged = new();

        [SerializeField] private SystemLanguage _defaultLanguage;

        [field: SerializeField] public SystemLanguage[] Languages { get; [UsedImplicitly] private set; }

        public Observable OnLocalizationChanged => _onLocalizationChanged.ToReadOnly();

        private SystemLanguage? _currentLanguage;

        public SystemLanguage Language {
            get => _currentLanguage ??= GetLanguage();
            set => SetLanguage(value);
        }

        private SystemLanguage GetLanguage() {
            if (OptionsRepository.TryGet(nameof(Language), out SystemLanguage language) && Languages.Contains(language)) {
                return language;
            }

            language = Languages.Contains(Application.systemLanguage) ? Application.systemLanguage : _defaultLanguage;
            OptionsRepository.Set(nameof(Language), language);
            return language;
        }

        private void SetLanguage(SystemLanguage value) {
            _currentLanguage = value;
            OptionsRepository.Set(nameof(Language), value);
            _onLocalizationChanged.Invoke();
        }
    }
}