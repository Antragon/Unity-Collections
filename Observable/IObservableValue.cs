namespace Collections.Observable {
    using System;

    public interface IObservableValue<T> {
        T? Value { get; }

        ObservableValueCallback<T> AddAndInvokeListener(Action<T?> action);

        ObservableValueCallback<T> AddAndInvokeChangeListener(Action<ValueArgs<T>> action);

        ObservableValueCallback<T> AddListener(Action<T?> action);

        ObservableValueCallback<T> AddChangeListener(Action<ValueArgs<T>> action);

        ObservableValueCallback<T> ListenOnce(Action<T?> action);

        ObservableValueCallback<T> ListenToChangeOnce(Action<ValueArgs<T>> action);
    }
}