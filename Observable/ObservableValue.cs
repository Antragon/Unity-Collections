namespace Collections.Observable {
    using System;

    public class ObservableValue<T> {
        private readonly ObservableProperty<T> _observableProperty;

        public ObservableValue(ObservableProperty<T> observableProperty) {
            _observableProperty = observableProperty;
        }

        public T Value => _observableProperty.Value;

        public ObservableValue<T> AddListener(Action<T> action) {
            _observableProperty.AddListener(action);
            return this;
        }

        public ObservableValue<T> AddAndInvokeListener(Action<T> action) {
            _observableProperty.AddListener(action);
            action(Value);
            return this;
        }

        public ObservableValue<T> ListenOnce(Action<T> action) {
            _observableProperty.ListenOnce(action);
            return this;
        }

        public ObservableValue<T> RemoveListener(Action<T> action) {
            _observableProperty.RemoveListener(action);
            return this;
        }

        public override string ToString() {
            return $"<{nameof(ObservableValue<T>)}>{Value}";
        }
    }
}