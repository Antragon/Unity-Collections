namespace Collections.Observable {
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
    }
}