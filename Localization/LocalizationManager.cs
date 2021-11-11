namespace Collections.Localization {
    using Components;
    using Observable;

    public class LocalizationManager : SingletonComponent<LocalizationManager> {
        public ObservableAction OnLocalizationChanged { get; } = new ObservableAction();
    }
}