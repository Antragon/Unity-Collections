namespace Collections.Observable {
    using System;
    using System.Collections.Generic;

    public class ObservableCallback<T> : IDisposable {
        private readonly IObservationExtender<T> _observationExtender;
        private readonly List<Action<T>> _actions = new();

        internal ObservableCallback(IObservationExtender<T> observationExtender) {
            _observationExtender = observationExtender;
        }

        public ObservableCallback<T> AddListener(Action<T> action) {
            _observationExtender.AddListener(action);
            _actions.Add(action);
            return this;
        }

        public ObservableCallback<T> AddAndInvokeListener(Action<T> action, T value) {
            _observationExtender.AddListener(action);
            action(value);
            _actions.Add(action);
            return this;
        }

        public ObservableCallback<T> ListenOnce(Action<T> action) {
            _observationExtender.ListenOnce(action);
            _actions.Add(action);
            return this;
        }

        public void Dispose() {
            _actions.ForEach(action => _observationExtender.RemoveListener(action));
        }
    }

    public class ObservableCallback : IDisposable {
        private readonly IObservationExtender _observationExtender;
        private readonly List<Action> _actions = new();

        internal ObservableCallback(IObservationExtender observationExtender) {
            _observationExtender = observationExtender;
        }

        public ObservableCallback AddAndInvokeListener(Action action) {
            action();
            _observationExtender.AddListener(action);
            _actions.Add(action);
            return this;
        }

        public ObservableCallback AddListener(Action action) {
            _observationExtender.AddListener(action);
            _actions.Add(action);
            return this;
        }

        public ObservableCallback ListenOnce(Action action) {
            _observationExtender.ListenOnce(action);
            _actions.Add(action);
            return this;
        }

        public void Dispose() {
            _actions.ForEach(action => _observationExtender.RemoveListener(action));
        }
    }
}