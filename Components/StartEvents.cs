namespace Collections.Components {
    using JetBrains.Annotations;
    using UnityEngine;
    using UnityEngine.Events;

    public class StartEvents : MonoBehaviour {
        [SerializeField] private bool _setGameObjectInactive;
        [field: SerializeField] public UnityEvent Events { get; [UsedImplicitly] private set; }

        private void Start() {
            if (_setGameObjectInactive) {
                gameObject.SetActive(false);
            }

            Events.Invoke();
        }
    }
}