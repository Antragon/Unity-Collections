﻿namespace Collections.Editor.Localization {
    using System.Linq;
    using Collections.Localization;
    using Collections.Localization.LocalizableComponents;
    using UnityEditor;
    using UnityEngine;
    using UnityEngine.UI;

    [CustomEditor(typeof(DropdownLocalization))]
    public class DropdownLocalizationEditor : Editor {
        private Dropdown _dropdown = null!;
        private DropdownLocalization _dropdownLocalization = null!;

        private void OnEnable() {
            _dropdownLocalization = (DropdownLocalization)target;
            _dropdown = _dropdownLocalization.GetComponent<Dropdown>();
        }

        public override void OnInspectorGUI() {
            base.OnInspectorGUI();

            if (Application.isPlaying) return;

            var optionDataList = _dropdownLocalization.LocalizableValues
                .Cast<LocalizableString>()
                .Select(x => new Dropdown.OptionData($"{x.Path}.{x.ValueKey}"))
                .ToList();
            _dropdown.options = optionDataList;
        }
    }
}