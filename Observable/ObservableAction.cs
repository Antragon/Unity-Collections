namespace Collections.Observable {
    using System;
    using System.Collections.Generic;

    public class ObservableAction<T> : IObservable<T>, IObservationExtender<T> {
        private readonly List<Action<T>> _oneTimeListeners = new();

        private Action<T> _action;

        public ObservableCallback<T> AddAndInvokeListener(Action<T> action, T value) {
            action(value);
            return AddListener(action);
        }

        public ObservableCallback<T> AddListener(Action<T> action) {
            return new ObservableCallback<T>(this).AddListener(action);
        }

        public ObservableCallback<T> ListenOnce(Action<T> action) {
            return new ObservableCallback<T>(this).ListenOnce(action);
        }

        public IObservable<T> RemoveListener(Action<T> action) {
            _action -= action;
            return this;
        }

        void IObservationExtender<T>.AddListener(Action<T> action) {
            _action += action;
        }

        void IObservationExtender<T>.ListenOnce(Action<T> action) {
            _action += action;
            _oneTimeListeners.Add(action);
        }

        public void Invoke(T value) {
            var oneTimeListeners = _oneTimeListeners.ToArray();
            _action?.Invoke(value);
            foreach (var action in oneTimeListeners) {
                _action -= action;
                _oneTimeListeners.Remove(action);
            }
        }
    }

    public class ObservableAction : IObservable, IObservationExtender {
        private readonly List<Action> _oneTimeListeners = new();

        private Action _action;

        public ObservableCallback AddAndInvokeListener(Action action) {
            action();
            return AddListener(action);
        }

        public ObservableCallback AddListener(Action action) {
            return new ObservableCallback(this).AddListener(action);
        }

        public ObservableCallback ListenOnce(Action action) {
            return new ObservableCallback(this).ListenOnce(action);
        }

        public IObservable RemoveListener(Action action) {
            _action -= action;
            return this;
        }

        void IObservationExtender.AddListener(Action action) {
            _action += action;
        }

        void IObservationExtender.ListenOnce(Action action) {
            _action += action;
            _oneTimeListeners.Add(action);
        }

        public void Invoke() {
            _action?.Invoke();
            foreach (var action in _oneTimeListeners.ToArray()) {
                _action -= action;
                _oneTimeListeners.Remove(action);
            }
        }
    }
}