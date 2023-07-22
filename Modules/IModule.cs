namespace Collections.Modules {
    using System.Collections;
    using Observable;

    public interface IModule {
        public ObservableAction<IEnumerator> CoroutineCallback { get; }

        void Start();

        void Update();
    }
}