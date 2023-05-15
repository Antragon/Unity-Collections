namespace Collections.Editor.Localization {
    using System.Linq;
    using Collections.Localization;
    using Collections.Localization.LocalizableComponents;
    using TMPro;
    using UnityEditor;
    using UnityEngine;

    [CustomEditor(typeof(DropdownProLocalization))]
    public class DropdownProLocalizationEditor : Editor {
        private TMP_Dropdown _dropdown;
        private DropdownProLocalization _dropdownLocalization;

        private void OnEnable() {
            _dropdownLocalization = (DropdownProLocalization)target;
            _dropdown = _dropdownLocalization.GetComponent<TMP_Dropdown>();
        }

        public override void OnInspectorGUI() {
            base.OnInspectorGUI();

            if (Application.isPlaying) return;
            if (_dropdownLocalization.LocalizableValues is null) return;

            var optionDataList = _dropdownLocalization.LocalizableValues
                .Cast<LocalizableString>()
                .Select(x => new TMP_Dropdown.OptionData($"{x.Path}.{x.ValueKey}"))
                .ToList();
            _dropdown.options = optionDataList;
        }
    }
}