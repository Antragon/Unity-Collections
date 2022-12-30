namespace Collections.Localization.LocalizableComponents {
    using System.Collections.Generic;
    using System.Linq;
    using Initialization;
    using UnityEngine;
    using UnityEngine.UI;

    public class DropdownLocalization : LocalizableComponent {
        [FromComponentInSingletons] private readonly LocalizationRepository _localizationRepository;

        [SerializeField] private List<LocalizableString> _localizableStrings;

        [FromComponentInChildren, SerializeField] private Dropdown _dropdown;

        private IEnumerable<ILocalizableValue> _localizableValues;

        public IEnumerable<ILocalizableValue> LocalizableValues {
            get => _localizableValues ??= _localizableStrings;
            set {
                _localizableValues = value;
                OnLocalizationChanged();
            }
        }

        protected override void OnLocalizationChanged() {
            InitializeOnce();
            var dropdownOptions = LocalizableValues
                .Select(x => x.GetLocalizedValue(_localizationRepository))
                .Select(x => new Dropdown.OptionData(x))
                .ToList();
            _dropdown.options = dropdownOptions;
        }
    }
}