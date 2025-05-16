namespace Collections.Components {
    using System.Globalization;
    using System.Linq;
    using Initialization;
    using Observable;
    using UnityEngine;
    using UnityEngine.UI;

    public class DecimalInputField : MonoBehaviour {
        private readonly ObservableAction<float> _onEditingEnded = new();
        private readonly ObservableAction<float> _onValueChanged = new();

        [FromComponent] private readonly InputField _inputField = null!;

        [SerializeField] private float _minimum;
        [SerializeField] private float _maximum;
        [SerializeField, Range(0, 10)] private int _decimalPlaces = 1;

        private int DecimalPlacesConversionValue => (int)Mathf.Pow(10, _decimalPlaces);

        public Observable<float> OnEditingEnded => _onEditingEnded.ReadOnly;
        public Observable<float> OnValueChanged => _onValueChanged.ReadOnly;

        public float Value {
            get => float.Parse(_inputField.text);
            set => _inputField.text = value.ToString(CultureInfo.CurrentCulture);
        }

        private void Awake() {
            this.Initialize();
            _inputField.contentType = InputField.ContentType.DecimalNumber;
            _inputField.onValueChanged.AddListener(OnValueChanging);
            _inputField.onEndEdit.AddListener(OnEndEdit);
        }

        private void OnValueChanging(string value) {
            if (!char.IsDigit(value.LastOrDefault()) || !float.TryParse(value, out var valueAsFloat)) return;
            valueAsFloat = Mathf.Clamp(valueAsFloat, _minimum, _maximum);
            valueAsFloat = Mathf.Floor(valueAsFloat * DecimalPlacesConversionValue) / DecimalPlacesConversionValue;
            Value = valueAsFloat;
            _onValueChanged.Invoke(valueAsFloat);
        }

        private void OnEndEdit(string value) {
            if (!float.TryParse(value, out var valueAsFloat)) {
                valueAsFloat = _minimum;
                Value = _minimum;
            }

            _onEditingEnded.Invoke(valueAsFloat);
        }

        public void SetMinimum(float minimum) {
            _minimum = minimum;
        }
    }
}