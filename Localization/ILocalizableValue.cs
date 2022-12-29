namespace Collections.Localization {
    public interface ILocalizableValue {
        string GetLocalizedValue(LocalizationRepository localizationRepository);
    }
}