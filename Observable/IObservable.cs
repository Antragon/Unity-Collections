namespace Collections.Observable {
    using System;

    public interface IObservable<T> {
        ObservableCallback<T> AddAndInvokeListener(Action<T> action, T value);

        ObservableCallback<T> AddListener(Action<T> action);

        ObservableCallback<T> ListenOnce(Action<T> action);

        IObservable<T> RemoveListener(Action<T> action);
    }

    public interface IObservable {
        ObservableCallback AddAndInvokeListener(Action action);

        ObservableCallback AddListener(Action action);

        ObservableCallback ListenOnce(Action action);

        IObservable RemoveListener(Action action);
    }
}