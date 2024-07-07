namespace Collections.Localization.LocalizableComponents {
    using Components;
    using Initialization;
    using Observable;
    using UnityEngine;

    public abstract class LocalizableComponent : MonoBehaviour {
        [FromComponentInSingletons] private readonly LocalizationManager _localizationManager;

        private bool _initialized;
        private ObservableCallback _callback;

        protected void Awake() {
            InitializeOnce();
        }

        protected void InitializeOnce() {
            if (_initialized) return;
            this.Initialize();
            _initialized = true;
        }

        protected void OnEnable() {
            _callback = _localizationManager.OnLocalizationChanged.AddListener(OnLocalizationChanged);
            OnLocalizationChanged();
        }

        protected void OnDisable() {
            if (GameControl.ApplicationIsQuitting) return;
            _callback?.Dispose();
        }

        protected abstract void OnLocalizationChanged();
    }
}