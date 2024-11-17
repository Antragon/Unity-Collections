namespace Collections.Components {
    using UnityEngine;
    using UnityEngine.Events;

    public class StartEvents : MonoBehaviour {
        [SerializeField] private bool _setGameObjectInactive;
        [field: SerializeField] public UnityEvent Events { get; private set; } = null!;

        private void Start() {
            if (_setGameObjectInactive) {
                gameObject.SetActive(false);
            }

            Events.Invoke();
        }
    }
}