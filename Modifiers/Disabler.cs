namespace Collections.Modifiers {
    using System.Collections.Generic;
    using Extensions;
    using Observable;
    using UnityEngine;

    public class Disabler {
        private readonly HashSet<object> _disableSources = new();
        private readonly List<Behaviour> _behaviours = new();
        private readonly List<Renderer> _renderers = new();
        private readonly List<GameObject> _gameObjects = new();
        private readonly ObservableProperty<bool> _hasDisableSources = new();

        public Disabler() {
            _hasDisableSources
                .AddListener(value => _behaviours.ForEach(b => b.Enable(!value)))
                .AddListener(value => _renderers.ForEach(r => r.Enable(!value)))
                .AddListener(value => _gameObjects.ForEach(g => g.SetActive(!value)));
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

        public static Disabler Create(params Renderer[] renderers) {
            var disabler = new Disabler();
            disabler._renderers.AddRange(renderers);
            return disabler;
        }

        public static Disabler Create(params GameObject[] gameObjects) {
            var disabler = new Disabler();
            disabler._gameObjects.AddRange(gameObjects);
            return disabler;
        }
    }
}