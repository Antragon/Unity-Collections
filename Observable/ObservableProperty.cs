namespace Collections.Observable {
    using System;

    [Serializable]
    public class ObservableProperty<T> : IObservable<T> {
        private T _value;

        public ObservableProperty()
            : this(default) { }

        internal ObservableAction<T> ObservableAction { get; } = new();

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
            ObservableAction.Invoke(_value);
        }

        public void SetSilently(T value) {
            _value = value;
        }

        public ObservableCallback<T> AddAndInvokeListener(Action<T> action) {
            action(Value);
            return AddListener(action);
        }

        public ObservableCallback<T> AddListener(Action<T> action) {
            return new ObservableCallback<T>(ObservableAction).AddListener(action);
        }

        public ObservableCallback<T> ListenOnce(Action<T> action) {
            return new ObservableCallback<T>(ObservableAction).ListenOnce(action);
        }

        public IObservable<T> RemoveListener(Action<T> action) {
            ObservableAction.RemoveListener(action);
            return this;
        }

        public override string ToString() {
            return $"<{nameof(ObservableProperty<T>)}>{Value}";
        }
    }
}