namespace Collections.Localization.LocalizableComponents {
    using Components;
    using Initialization;
    using UnityEngine;

    public abstract class LocalizableComponent : MonoBehaviour {
        [FromComponentInSingletons] private readonly LocalizationManager _localizationManager;

        private bool _initialized;

        protected void Awake() {
            InitializeOnce();
        }

        protected void InitializeOnce() {
            if (_initialized) return;
            this.Initialize();
            _initialized = true;
        }

        protected void OnEnable() {
            _localizationManager.OnLocalizationChanged.AddListener(OnLocalizationChanged);
            OnLocalizationChanged();
        }

        protected void OnDisable() {
            if (GameControl.ApplicationIsQuitting) return;
            _localizationManager.OnLocalizationChanged.RemoveListener(OnLocalizationChanged);
        }

        protected abstract void OnLocalizationChanged();
    }
}