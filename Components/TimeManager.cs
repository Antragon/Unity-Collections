namespace Collections.Components {
    using System.Collections.Generic;
    using System.Linq;
    using UnityEngine;
    using UnityEngine.InputSystem;

    public class TimeManager : MonoBehaviour {
        private const float _slowMoTimeScale = 0.1f;

        private readonly HashSet<object> _timeStoppers = new();

        [SerializeField] private float _timeScaleDefault = 1;

        private float _timeScaleMultiplier = 1;

        public float TimeScaleMultiplier {
            get => _timeScaleMultiplier;
            set {
                _timeScaleMultiplier = value;
                UpdateTimeScale();
            }
        }

        public void AddTimeStopper(object timeStopper) {
            _timeStoppers.Add(timeStopper);
            UpdateTimeScale();
        }

        public void RemoveTimeStopper(object timeStopper) {
            _timeStoppers.Remove(timeStopper);
            UpdateTimeScale();
        }

        private void Awake() {
            UpdateTimeScale();
        }

        private void OnValidate() {
            if (Application.isPlaying) {
                UpdateTimeScale();
            }
        }

        private void UpdateTimeScale() {
            Time.timeScale = _timeStoppers.Any() ? 0 : _timeScaleDefault * TimeScaleMultiplier;
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