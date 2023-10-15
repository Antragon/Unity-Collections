namespace Collections.Observable {
    using System;
    using System.Collections.Generic;

    public static class ObservableExtensions {
        public static Observable ToObservable(this ObservableAction observableAction) {
            return new Observable(observableAction);
        }

        public static Observable<T> ToObservable<T>(this ObservableAction<T> observableAction) {
            return new Observable<T>(observableAction);
        }

        public static ObservableValue<T> ToObservable<T>(this ObservableProperty<T> subject) {
            return new ObservableValue<T>(subject);
        }

        public static void Dispose(this List<IDisposable> disposables) {
            disposables.ForEach(d => d.Dispose());
            disposables.Clear();
        }
    }
}