namespace Collections.Components {
    using System.Globalization;
    using System.Linq;
    using Initialization;
    using Observable;
    using UnityEngine;
    using UnityEngine.UI;

    public class DecimalInputField : MonoBehaviour {
        private readonly ObservableAction<float> _onEditingEnded = new ObservableAction<float>();

        [FromComponent] private InputField _inputField;

        [SerializeField] private float _minimum;
        [SerializeField] private float _maximum;
        [SerializeField, Range(0, 10)] private int _decimalPlaces = 1;

        public string Text {
            get => _inputField.text;
            set => _inputField.text = value;
        }

        public Observable<float> OnEditingEnded => _onEditingEnded.ToObservable();

        private void Awake() {
            this.Initialize();
            _inputField.contentType = InputField.ContentType.DecimalNumber;
            _inputField.onValueChanged.AddListener(OnValueChanged);
            _inputField.onEndEdit.AddListener(OnEndEdit);
        }

        private void OnValueChanged(string value) {
            if (!char.IsDigit(value.LastOrDefault()) || !float.TryParse(value, out var valueAsFloat)) return;
            valueAsFloat = Mathf.Clamp(valueAsFloat, _minimum, _maximum);
            valueAsFloat = Mathf.Floor(valueAsFloat * DecimalPlacesConversionValue) / DecimalPlacesConversionValue;
            Text = valueAsFloat.ToString(CultureInfo.CurrentCulture);
        }

        private int DecimalPlacesConversionValue => (int)Mathf.Pow(10, _decimalPlaces);

        private void OnEndEdit(string value) {
            float.TryParse(value, out var valueAsFloat);
            _onEditingEnded.Invoke(valueAsFloat);
        }
    }
}