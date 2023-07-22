namespace Collections.Modules {
    using System;
    using System.Collections.Generic;
    using Initialization;
    using MoreLinq;
    using UnityEditor;
    using UnityEngine;

    public abstract class ModuleRunner : MonoBehaviour {
        private static readonly Type _moduleType = typeof(IModule);

        private readonly List<IModule> _modules = new();

        public abstract MonoScript[] ModuleScripts { get; set; }

        private void Awake() {
            this.Initialize();
            ModuleScripts.ForEach(CreateModule);
        }

        private void CreateModule(MonoScript monoScript) {
            var type = monoScript.GetClass();
            if (!_moduleType.IsAssignableFrom(type)) {
                Debug.LogWarning($"Cannot create instance of {type}. Must inherit from {_moduleType}");
                return;
            }

            try {
                var module = (IModule)Activator.CreateInstance(type);
                module.Initialize(this);
                module.CoroutineCallback.AddListener(coroutine => StartCoroutine(coroutine));
                _modules.Add(module);
            } catch (MissingMethodException e) {
                Debug.LogWarning(e.Message);
            }
        }

        protected virtual void Start() {
            _modules.ForEach(m => m.Start());
        }

        protected virtual void Update() {
            _modules.ForEach(m => m.Update());
        }
    }
}