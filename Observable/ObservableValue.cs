namespace Collections.Observable {
    using System;

    public class ObservableValue<T> : IObservable<T> {
        private readonly ObservableProperty<T> _observableProperty;

        public ObservableValue(ObservableProperty<T> observableProperty) {
            _observableProperty = observableProperty;
        }

        public T Value => _observableProperty.Value;

        public ObservableCallback<T> AddAndInvokeListener(Action<T> action) {
            action(Value);
            return AddListener(action);
        }

        public ObservableCallback<T> AddListener(Action<T> action) {
            return new ObservableCallback<T>(_observableProperty.ObservableAction).AddListener(action);
        }

        public ObservableCallback<T> ListenOnce(Action<T> action) {
            return new ObservableCallback<T>(_observableProperty.ObservableAction).ListenOnce(action);
        }

        public IObservable<T> RemoveListener(Action<T> action) {
            _observableProperty.RemoveListener(action);
            return this;
        }

        public override string ToString() {
            return $"<{nameof(ObservableValue<T>)}>{Value}";
        }
    }
}