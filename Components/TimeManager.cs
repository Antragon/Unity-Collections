namespace Collections.Components {
    using UnityEngine;
    using UnityEngine.InputSystem;

    public class TimeManager : MonoBehaviour {
        private const float _slowMoTimeScale = 0.1f;

        [SerializeField] private float _timeScaleDefault = 1;

        private float _timeScaleMultiplier = 1;

        public float TimeScaleMultiplier {
            get => _timeScaleMultiplier;
            set => SetTimeScaleMultiplier(value);
        }

        private void SetTimeScaleMultiplier(float value) {
            _timeScaleMultiplier = value;
            UpdateTimeScale();
        }

        private void Awake() {
            UpdateTimeScale();
        }

        private void OnValidate() {
            UpdateTimeScale();
        }

        private void UpdateTimeScale() {
            Time.timeScale = _timeScaleDefault * TimeScaleMultiplier;
        }

        private void Update() {
            UpdateSlowMotion();
        }

        private void UpdateSlowMotion() {
            if (Keyboard.current[Key.Tab].wasPressedThisFrame) {
                TimeScaleMultiplier = _slowMoTimeScale;
            } else if (Keyboard.current[Key.Tab].wasReleasedThisFrame) {
                TimeScaleMultiplier = 1;
            }
        }
    }
}