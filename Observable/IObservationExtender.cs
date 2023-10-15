namespace Collections.Observable {
    using System;

    internal interface IObservationExtender {
        void AddListener(Action action);

        void ListenOnce(Action action);

        IObservable RemoveListener(Action action);
    }

    internal interface IObservationExtender<T> {
        void AddListener(Action<T> action);

        void ListenOnce(Action<T> action);

        IObservable<T> RemoveListener(Action<T> action);
    }
}