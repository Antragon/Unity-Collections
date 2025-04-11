namespace Collections.Observable {
    using System;
    using System.Collections.Generic;

    public static class ObservableExtensions {
        public static void Dispose(this List<IDisposable> disposables) {
            disposables.ForEach(d => d.Dispose());
            disposables.Clear();
        }
    }
}