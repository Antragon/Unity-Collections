namespace Collections.Observable {
    using System;
    using System.Collections.Generic;

    public static class ObservableExtensions {
        public static Observable ToObservable(this ObservableAction subject) {
            return new Observable(subject);
        }

        public static Observable<T> ToObservable<T>(this ObservableAction<T> subject) {
            return new Observable<T>(subject);
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