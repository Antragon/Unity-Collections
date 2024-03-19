namespace Collections.Components {
    using Observable;
    using UnityEngine;
#if ENABLE_INPUT_SYSTEM
    using UnityEngine.InputSystem;
#endif

    public class GameControl : MonoBehaviour {
        public static bool ApplicationIsQuitting { get; private set; }

        private readonly ObservableProperty<bool> _overlayIsActive = new();

#if ENABLE_INPUT_SYSTEM
        [SerializeField] private Key _overlayKey;
#else
        [SerializeField] private KeyCode _overlayKey;
#endif

        private int _fps;
        private float _cooldown = 1;
        private string _fpsText = "999 FPS";

        public ObservableValue<bool> OverlayIsActive => _overlayIsActive.ToObservable();

        private void Awake() {
            DontDestroyOnLoad(gameObject);
        }

        private void OnApplicationQuit() {
            ApplicationIsQuitting = true;
        }

        private void Update() {
            UpdateFpsCounter();
            UpdateOverlay();
        }

        private void UpdateFpsCounter() {
            _cooldown -= Time.unscaledDeltaTime;
            _fps += 1;

            if (_cooldown <= 0) {
                _fpsText = $"{_fps.ToString()} FPS";
                _cooldown = 1;
                _fps = 0;
            }
        }

        private void UpdateOverlay() {
#if ENABLE_INPUT_SYSTEM
            var overlayButtonPressed = Keyboard.current[_overlayKey].wasPressedThisFrame;
#else
            var overlayButtonPressed = Input.GetKeyDown(_overlayKey);
#endif
            if (overlayButtonPressed) {
                _overlayIsActive.Value = !_overlayIsActive.Value;
            }
        }

        private void OnGUI() {
            if (!OverlayIsActive.Value) return;
            var customStyle = new GUIStyle { fontSize = 20, normal = { textColor = Color.red } };

            GUI.Label(new Rect(new Vector2(Screen.width - 90, 10), new Vector2(80, 20)), _fpsText, customStyle);
        }
    }
}