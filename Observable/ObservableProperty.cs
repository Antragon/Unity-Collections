namespace Collections.Observable {
    using System;

    [Serializable]
    public class ObservableProperty<T> {
        private readonly ObservableAction<T> _onValueChanged = new ObservableAction<T>();

        private T _value;

        public ObservableProperty()
            : this(default) { }

        public ObservableProperty(T value) {
            Value = value;
        }

        public T Value {
            get => _value;
            set => SetValue(value);
        }

        public void SetValue(T value) {
            if (Equals(_value, value)) return;
            SetAndInvoke(value);
        }

        public void SetAndInvoke(T value) {
            _value = value;
            Invoke();
        }

        public void Invoke() {
            _onValueChanged.Invoke(_value);
        }

        public void SetSilently(T value) {
            _value = value;
        }

        public ObservableProperty<T> AddListener(Action<T> onValueChanged) {
            _onValueChanged.AddListener(onValueChanged);
            return this;
        }

        public ObservableProperty<T> AddAndInvokeListener(Action<T> onValueChanged) {
            _onValueChanged.AddListener(onValueChanged);
            onValueChanged(Value);
            return this;
        }

        public ObservableProperty<T> ListenOnce(Action<T> onValueChanged) {
            _onValueChanged.ListenOnce(onValueChanged);
            return this;
        }

        public ObservableProperty<T> RemoveListener(Action<T> onValueChanged) {
            _onValueChanged.RemoveListener(onValueChanged);
            return this;
        }

        public ObservableProperty<T> Clone() {
            return new ObservableProperty<T>(Value);
        }
    }
}