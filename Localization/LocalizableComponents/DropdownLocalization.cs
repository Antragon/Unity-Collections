namespace Collections.Localization.LocalizableComponents {
    using System.Collections.Generic;
    using System.Linq;
    using UnityEngine;
    using UnityEngine.UI;

    public class DropdownLocalization : LocalizableComponent {
        [SerializeField] private List<LocalizableString> _localizableStrings;

        private Dropdown _dropdown;
        private IEnumerable<ILocalizableValue> _localizableValues;

        public IEnumerable<ILocalizableValue> LocalizableValues {
            get => _localizableValues ??= _localizableStrings;
            set {
                _localizableValues = value;
                OnLocalizationChanged();
            }
        }

        protected override void OnLocalizationChanged() {
            if (!_dropdown) {
                _dropdown = GetComponent<Dropdown>();
            }

            var dropdownOptions = LocalizableValues
                .Select(x => x.GetLocalizedValue())
                .Select(x => new Dropdown.OptionData(x))
                .ToList();
            _dropdown.options = dropdownOptions;
        }
    }
}