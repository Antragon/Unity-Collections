namespace Collections.Observable {
    using System;

    public class ObservableValue<T> : IObservableValue<T> {
        protected readonly ObservableProperty<T> _observableProperty;

        public ObservableValue(ObservableProperty<T> observableProperty) {
            _observableProperty = observableProperty;
        }

        public T Value => _observableProperty.Value;
        public ObservableValueCallback<T> AddAndInvokeListener(Action<T> action) {
            return _observableProperty.AddAndInvokeListener(action);
        }

        public ObservableValueCallback<T> AddAndInvokeChangeListener(Action<ValueArgs<T>> action) {
            return _observableProperty.AddAndInvokeChangeListener(action);
        }

        public ObservableValueCallback<T> AddListener(Action<T> action) {
            return _observableProperty.AddListener(action);
        }

        public ObservableValueCallback<T> AddChangeListener(Action<ValueArgs<T>> action) {
            return _observableProperty.AddChangeListener(action);
        }

        public ObservableValueCallback<T> ListenOnce(Action<T> action) {
            return _observableProperty.ListenOnce(action);
        }

        public ObservableValueCallback<T> ListenToChangeOnce(Action<ValueArgs<T>> action) {
            return _observableProperty.ListenToChangeOnce(action);
        }

        public override string ToString() {
            return $"<{nameof(ObservableValue<T>)}>{Value}";
        }
    }
}