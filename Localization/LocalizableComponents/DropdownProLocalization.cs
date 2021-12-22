namespace Collections.Localization.LocalizableComponents {
    using System.Collections.Generic;
    using System.Linq;
    using Initialization;
    using TMPro;
    using UnityEngine;
    using UnityEngine.Serialization;

    public class DropdownProLocalization : LocalizableComponent {
        [FormerlySerializedAs("localizableStrings")] [SerializeField] private List<LocalizableString> _localizableStrings;

        private IEnumerable<ILocalizableValue> _localizableValues;
        private TMP_Dropdown _dropdown;

        public IEnumerable<ILocalizableValue> LocalizableValues {
            get => _localizableValues ??= _localizableStrings;
            set {
                _localizableValues = value;
                OnLocalizationChanged();
            }
        }

        protected override void OnLocalizationChanged() {
            if (!_dropdown) {
                _dropdown = GetComponent<TMP_Dropdown>();
            }

            var dropdownOptions = LocalizableValues
                .Select(x => x.GetLocalizedValue())
                .Select(x => new TMP_Dropdown.OptionData(x))
                .ToList();
            _dropdown.options = dropdownOptions;
        }
    }
}