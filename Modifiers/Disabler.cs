namespace Collections.Modifiers {
    using System;
    using System.Collections.Generic;
    using Extensions;
    using MoreLinq;
    using UnityEngine;

    public class Disabler {
        private readonly Action<bool> _setEnable;
        private readonly HashSet<object> _disableSources = new();

        public Disabler(params Behaviour[] behaviours) {
            _setEnable = value => behaviours.ForEach(b => b.Enable(value));
        }

        public Disabler(params GameObject[] gameObjects) {
            _setEnable = value => gameObjects.ForEach(b => b.SetActive(value));
        }

        public Disabler(Action<bool> setEnable) {
            _setEnable = setEnable;
        }

        public void Set(bool value, object source) {
            if (value) {
                _disableSources.Add(source);
            } else {
                _disableSources.Remove(source);
            }

            _setEnable(_disableSources.Count == 0);
        }
    }
}