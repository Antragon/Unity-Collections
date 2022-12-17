namespace Collections.Localization {
    using System;

    public class UnlocalizedCallbackValue : ILocalizableValue {
        private readonly Func<string> _getValue;

        public UnlocalizedCallbackValue(Func<string> getValue) {
            _getValue = getValue;
        }

        public string GetLocalizedValue() => _getValue();
    }
}