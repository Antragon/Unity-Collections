namespace Collections.Localization.LocalizableComponents {
    using System.Collections.Generic;
    using System.Linq;
    using TMPro;
    using UnityEngine;

    [RequireComponent(typeof(TMP_Dropdown))]
    public class DropdownProLocalization : LocalizableComponent {
        private IEnumerable<ILocalizableValue> _localizableValues;
        private TMP_Dropdown _dropdown;

        [SerializeField] private List<LocalizableString> localizableStrings;

        public IEnumerable<ILocalizableValue> LocalizableValues {
            get => _localizableValues ??= localizableStrings;
            set {
                _localizableValues = value;
                OnLocalizationChanged();
            }
        }

        private void Awake() {
            _dropdown = GetComponent<TMP_Dropdown>();
        }

        protected override void OnLocalizationChanged() {
            var dropdownOptions = LocalizableValues
                .Select(x => x.GetLocalizedValue())
                .Select(x => new TMP_Dropdown.OptionData(x))
                .ToList();
            _dropdown.options = dropdownOptions;
        }
    }
}