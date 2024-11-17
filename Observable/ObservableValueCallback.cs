namespace Collections.Observable {
    using System;
    using System.Collections.Generic;

    public class ObservableValueCallback<T> : IObservableValue<T>, IDisposable {
        private readonly IObservationValueExtender<T> _observationValueExtender;
        private readonly List<Action<ValueArgs<T>>> _actions = new();

        internal ObservableValueCallback(IObservationValueExtender<T> observationValueExtender) {
            _observationValueExtender = observationValueExtender;
        }

        public T? Value => _observationValueExtender.Value;

        public ObservableValueCallback<T> AddAndInvokeListener(Action<T?> action) {
            return AddAndInvokeChangeListener(args => action(args.Value));
        }

        public ObservableValueCallback<T> AddAndInvokeChangeListener(Action<ValueArgs<T>> action) {
            var value = _observationValueExtender.Value;
            var valueChange = new ValueArgs<T>(value, value);
            action(valueChange);
            return AddChangeListener(action);
        }

        public ObservableValueCallback<T> AddListener(Action<T?> action) {
            return AddChangeListener(args => action(args.Value));
        }

        public ObservableValueCallback<T> AddChangeListener(Action<ValueArgs<T>> action) {
            _observationValueExtender.AddListener(action);
            _actions.Add(action);
            return this;
        }

        public ObservableValueCallback<T> ListenOnce(Action<T?> action) {
            return ListenToChangeOnce(args => action(args.Value));
        }

        public ObservableValueCallback<T> ListenToChangeOnce(Action<ValueArgs<T>> action) {
            _observationValueExtender.ListenOnce(action);
            _actions.Add(action);
            return this;
        }

        public void Dispose() {
            _actions.ForEach(action => _observationValueExtender.RemoveListener(action));
            _actions.Clear();
        }
    }
}