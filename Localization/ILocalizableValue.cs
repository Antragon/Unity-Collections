namespace Collections.Localization {
    public interface ILocalizableValue {
        string ValueKey { get; }

        string GetLocalizedValue();
    }
}