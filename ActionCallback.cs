namespace Collections {
    using System;

    public class ActionCallback : IDisposable {
        private readonly Action _dispose;

        public ActionCallback(Action dispose) {
            _dispose = dispose;
        }

        public void Dispose() {
            _dispose.Invoke();
        }
    }
}