namespace Collections.Observable {
    using System;
    using System.Collections.Generic;

    public class ObservableAction<T> {
        private readonly List<Action<T>> _oneTimeListeners = new List<Action<T>>();

        private Action<T> _onEvent;

        public ObservableAction<T> AddListener(Action<T> action) {
            _onEvent += action;
            return this;
        }

        public ObservableAction<T> ListenOnce(Action<T> action) {
            _onEvent += action;
            _oneTimeListeners.Add(action);
            return this;
        }

        public ObservableAction<T> RemoveListener(Action<T> action) {
            _onEvent -= action;
            return this;
        }

        public void Invoke(T value) {
            var oneTimeListeners = _oneTimeListeners.ToArray();
            _onEvent?.Invoke(value);
            foreach (var action in oneTimeListeners) {
                _onEvent -= action;
                _oneTimeListeners.Remove(action);
            }
        }
    }

    public class ObservableAction {
        private readonly List<Action> _oneTimeListeners = new List<Action>();

        private Action _onEvent;

        public ObservableAction AddListener(Action action) {
            _onEvent += action;
            return this;
        }

        public ObservableAction ListenOnce(Action action) {
            _onEvent += action;
            _oneTimeListeners.Add(action);
            return this;
        }

        public ObservableAction RemoveListener(Action action) {
            _onEvent -= action;
            return this;
        }

        public void Invoke() {
            _onEvent?.Invoke();
            foreach (var action in _oneTimeListeners.ToArray()) {
                _onEvent -= action;
                _oneTimeListeners.Remove(action);
            }
        }
    }
}