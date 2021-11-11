namespace Collections.Observable {
    using System;

    public class Observable {
        private readonly ObservableAction _observableAction;

        public Observable(ObservableAction observableAction) {
            _observableAction = observableAction;
        }

        public Observable AddListener(Action action) {
            _observableAction.AddListener(action);
            return this;
        }

        public Observable ListenOnce(Action action) {
            _observableAction.ListenOnce(action);
            return this;
        }

        public Observable RemoveListener(Action action) {
            _observableAction.RemoveListener(action);
            return this;
        }
    }

    public class Observable<T> {
        private readonly ObservableAction<T> _observableAction;

        public Observable(ObservableAction<T> observableAction) {
            _observableAction = observableAction;
        }

        public Observable<T> AddListener(Action<T> action) {
            _observableAction.AddListener(action);
            return this;
        }

        public Observable<T> ListenOnce(Action<T> action) {
            _observableAction.ListenOnce(action);
            return this;
        }

        public Observable<T> RemoveListener(Action<T> action) {
            _observableAction.RemoveListener(action);
            return this;
        }
    }
}