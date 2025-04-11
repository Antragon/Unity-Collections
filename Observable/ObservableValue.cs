namespace Collections.Observable {
    using System;

    public class ObservableValue<T> : IObservableValue<T> {
        private readonly IObservableValue<T> _observableValue;

        internal ObservableValue(IObservableValue<T> observableValue) {
            _observableValue = observableValue;
        }

        public T? Value => _observableValue.Value;

        public ObservableValueCallback<T> AddAndInvokeListener(Action<T?> action) {
            return _observableValue.AddAndInvokeListener(action);
        }

        public ObservableValueCallback<T> AddAndInvokeChangeListener(Action<ValueArgs<T>> action) {
            return _observableValue.AddAndInvokeChangeListener(action);
        }

        public ObservableValueCallback<T> AddListener(Action<T?> action) {
            return _observableValue.AddListener(action);
        }

        public ObservableValueCallback<T> AddChangeListener(Action<ValueArgs<T>> action) {
            return _observableValue.AddChangeListener(action);
        }

        public ObservableValueCallback<T> ListenOnce(Action<T?> action) {
            return _observableValue.ListenOnce(action);
        }

        public ObservableValueCallback<T> ListenToChangeOnce(Action<ValueArgs<T>> action) {
            return _observableValue.ListenToChangeOnce(action);
        }

        public override string ToString() {
            return $"<{nameof(ObservableValue<T>)}>{Value}";
        }
    }
}