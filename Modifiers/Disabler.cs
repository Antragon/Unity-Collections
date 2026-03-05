namespace Collections.Modifiers {
    using System.Collections.Generic;
    using Extensions;
    using Observable;
    using UnityEngine;

    public class Disabler {
        private readonly HashSet<object> _disableSources = new();
        private readonly List<Behaviour> _behaviours = new();
        private readonly List<GameObject> _gameObjects = new();
        private readonly ObservableProperty<bool> _hasDisableSources;

        public Disabler() {
            _hasDisableSources = new ObservableProperty<bool>();
            _hasDisableSources
                .AddListener(value => _behaviours.ForEach(b => b.Enable(!value)))
                .AddListener(value => _gameObjects.ForEach(b => b.SetActive(!value)));
        }

        public ObservableValue<bool> HasDisableSources => _hasDisableSources.ReadOnly;

        public void Set(bool value, object source) {
            if (value) {
                _disableSources.Add(source);
            } else {
                _disableSources.Remove(source);
            }

            _hasDisableSources.Value = _disableSources.Count > 0;
        }

        public static Disabler Create(params Behaviour[] behaviours) {
            var disabler = new Disabler();
            disabler._behaviours.AddRange(behaviours);
            return disabler;
        }

        public static Disabler Create(params GameObject[] gameObjects) {
            var disabler = new Disabler();
            disabler._gameObjects.AddRange(gameObjects);
            return disabler;
        }
    }
}