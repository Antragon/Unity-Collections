namespace Collections.Localization {
    public interface ILocalizableValue {
        bool IsLocalized(LocalizationRepository localizationRepository);

        string GetLocalizedValue(LocalizationRepository localizationRepository);
    }
}