namespace Collections.Components {
    using Global;
    using Observable;
    using UnityEngine;

    public class DestroyNotifier : MonoBehaviour {
        public ObservableAction OnDestroying { get; } = new ObservableAction();

        private void OnDestroy() {
            if (GameControl.ApplicationIsQuitting || SceneLoader.IsChangingScene) return;
            OnDestroying.Invoke();
        }
    }
}