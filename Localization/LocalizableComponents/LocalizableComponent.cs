namespace Collections.Localization.LocalizableComponents {
    using System.Collections;
    using MainMenu;
    using UnityEngine;

    public abstract class LocalizableComponent : MonoBehaviour {
        protected void OnEnable() {
            //TODO: Make Localization Manager
            GameOptions.OnLocalizationChanged += OnLocalizationChanged;
            StartCoroutine(LocalizeLate());
        }

        private IEnumerator LocalizeLate() {
            yield return new WaitForEndOfFrame();
            OnLocalizationChanged();
        }

        protected void OnDisable() {
            GameOptions.OnLocalizationChanged -= OnLocalizationChanged;
        }

        protected abstract void OnLocalizationChanged();
    }
}