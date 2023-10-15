namespace Collections.Observable {
    using System;

    public class Observable<T> : IObservable<T> {
        private readonly ObservableAction<T> _observableAction;

        public Observable(ObservableAction<T> observableAction) {
            _observableAction = observableAction;
        }

        public ObservableCallback<T> AddAndInvokeListener(Action<T> action, T value) {
            action(value);
            return AddListener(action);
        }

        public ObservableCallback<T> AddListener(Action<T> action) {
            return new ObservableCallback<T>(_observableAction).AddListener(action);
        }

        public ObservableCallback<T> ListenOnce(Action<T> action) {
            return new ObservableCallback<T>(_observableAction).ListenOnce(action);
        }

        public IObservable<T> RemoveListener(Action<T> action) {
            _observableAction.RemoveListener(action);
            return this;
        }
    }

    public class Observable : IObservable {
        private readonly ObservableAction _observableAction;

        public Observable(ObservableAction observableAction) {
            _observableAction = observableAction;
        }

        public ObservableCallback AddAndInvokeListener(Action action) {
            action();
            return AddListener(action);
        }

        public ObservableCallback AddListener(Action action) {
            return new ObservableCallback(_observableAction).AddListener(action);
        }

        public ObservableCallback ListenOnce(Action action) {
            return new ObservableCallback(_observableAction).ListenOnce(action);
        }

        public IObservable RemoveListener(Action action) {
            _observableAction.RemoveListener(action);
            return this;
        }
    }
}