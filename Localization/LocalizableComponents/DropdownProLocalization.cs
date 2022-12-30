namespace Collections.Localization.LocalizableComponents {
    using System.Collections.Generic;
    using System.Linq;
    using Initialization;
    using TMPro;
    using UnityEngine;
    using UnityEngine.Serialization;

    public class DropdownProLocalization : LocalizableComponent {
        [FromComponentInSingletons] private readonly LocalizationRepository _localizationRepository;

        [FormerlySerializedAs("localizableStrings")] [SerializeField] private List<LocalizableString> _localizableStrings;

        [FromComponentInChildren, SerializeField] private TMP_Dropdown _dropdown;

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
                .Select(x => new TMP_Dropdown.OptionData(x))
                .ToList();
            _dropdown.options = dropdownOptions;
        }
    }
}