namespace Collections.Observable {
    using System;

    public interface IObservationValueExtender<T> {
        T? Value { get; }

        void AddListener(Action<ValueArgs<T>> action);

        void ListenOnce(Action<ValueArgs<T>> action);

        void RemoveListener(Action<ValueArgs<T>> action);
    }
}