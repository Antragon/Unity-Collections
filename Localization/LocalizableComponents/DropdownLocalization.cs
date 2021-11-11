namespace Collections.Localization.LocalizableComponents {
    using System.Collections.Generic;
    using System.Linq;
    using UnityEngine;
    using UnityEngine.UI;

    [RequireComponent(typeof(Dropdown))]
    public class DropdownLocalization : LocalizableComponent {
        private IEnumerable<ILocalizableValue> _localizableValues;
        private Dropdown _dropdown;

        [SerializeField] private List<LocalizableString> _localizableStrings;

        public IEnumerable<ILocalizableValue> LocalizableValues {
            get => _localizableValues ??= _localizableStrings;
            set {
                _localizableValues = value;
                OnLocalizationChanged();
            }
        }

        private void Awake() {
            _dropdown = GetComponent<Dropdown>();
        }

        protected override void OnLocalizationChanged() {
            var dropdownOptions = LocalizableValues
                .Select(x => x.GetLocalizedValue())
                .Select(x => new Dropdown.OptionData(x))
                .ToList();
            _dropdown.options = dropdownOptions;
        }
    }
}