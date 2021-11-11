namespace Collections.Localization.LocalizableComponents {
    using System.Collections;
    using UnityEngine;

    public abstract class LocalizableComponent : MonoBehaviour {
        protected void OnEnable() {
            LocalizationManager.Self.OnLocalizationChanged.AddListener(OnLocalizationChanged);
            StartCoroutine(LocalizeLate());
        }

        private IEnumerator LocalizeLate() {
            yield return new WaitForEndOfFrame();
            OnLocalizationChanged();
        }

        protected void OnDisable() {
            LocalizationManager.Self.OnLocalizationChanged.RemoveListener(OnLocalizationChanged);
        }

        protected abstract void OnLocalizationChanged();
    }
}