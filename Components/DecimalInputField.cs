namespace Collections.Components {
    using System.Globalization;
    using System.Linq;
    using Initialization;
    using Observable;
    using UnityEngine;
    using UnityEngine.UI;

    public class DecimalInputField : MonoBehaviour {
        private readonly ObservableAction<float> _onEditingEnded = new();
        private readonly ObservableProperty<float> _number = new();

        [FromComponent] private readonly InputField _inputField = null!;

        [SerializeField] private float _minimum;
        [SerializeField] private float _maximum;
        [SerializeField, Range(0, 10)] private int _decimalPlaces = 1;
        [SerializeField] private string? _format;

        private int DecimalPlacesConversionValue => (int)Mathf.Pow(10, _decimalPlaces);

        public Observable<float> OnEditingEnded => _onEditingEnded.ReadOnly;

        public ObservableValue<float> Number => _number.ReadOnly;

        private void Awake() {
            this.Initialize();
            Number.AddAndInvokeListener(value => _inputField.text = value.ToString(_format, CultureInfo.CurrentCulture));
            _inputField.contentType = InputField.ContentType.DecimalNumber;
            _inputField.onValueChanged.AddListener(OnValueChanging);
            _inputField.onEndEdit.AddListener(OnEndEdit);
        }

        private void OnValueChanging(string value) {
            if (!char.IsDigit(value.LastOrDefault()) || !float.TryParse(value, out var valueAsFloat)) return;
            Set(valueAsFloat);
        }

        private void OnEndEdit(string value) {
            if (!float.TryParse(value, out var valueAsFloat)) {
                valueAsFloat = _minimum;
                _number.Value = _minimum;
            }

            _onEditingEnded.Invoke(valueAsFloat);
        }

        public void Increment(float increment) {
            Set(_number.Value + increment);
        }

        public void Set(float value) {
            value = Mathf.Clamp(value, _minimum, _maximum);
            value = Mathf.Floor(value * DecimalPlacesConversionValue) / DecimalPlacesConversionValue;
            _inputField.SetTextWithoutNotify(value.ToString(_format, CultureInfo.CurrentCulture));
            _number.Value = value;
        }

        public void SetMinimum(float minimum) {
            _minimum = minimum;
        }
    }
}