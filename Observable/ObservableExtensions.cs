namespace Collections.Observable {
    using System;
    using System.Collections.Generic;

    public static class ObservableExtensions {
        public static void Dispose(this List<IDisposable> disposables) {
            disposables.ForEach(d => d.Dispose());
            disposables.Clear();
        }

        public static ObservableCallback<T> AddAndInvokeListener<T>(this IObservable<T> observable, Action action) {
            return observable.AddAndInvokeListener(_ => action(), default!);
        }

        public static ObservableCallback<T> AddListener<T>(this IObservable<T> observable, Action action) {
            return observable.AddListener(_ => action());
        }

        public static ObservableValueCallback<T> AddAndInvokeListener<T>(this IObservableValue<T> observableValue, Action action) {
            return observableValue.AddAndInvokeListener(_ => action());
        }

        public static ObservableValueCallback<T> AddListener<T>(this IObservableValue<T> observable, Action action) {
            return observable.AddListener(_ => action());
        }
    }
}