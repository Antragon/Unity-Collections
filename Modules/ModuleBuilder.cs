namespace Collections.Modules {
    using System;
    using UnityEditor;
    using UnityEngine;

    [Serializable]
    public class ModuleBuilder {
#if UNITY_EDITOR
        [SerializeField] private MonoScript _moduleScript;

        public void UpdateType() {
            _moduleType = _moduleScript ? _moduleScript.GetClass().AssemblyQualifiedName : null;
        }
#endif

        private static readonly Type _moduleInterfaceType = typeof(IModule);

        [SerializeField] private string _moduleType;

        public IModule CreateModule() {
            var moduleType = Type.GetType(_moduleType)!;
            if (!_moduleInterfaceType.IsAssignableFrom(moduleType)) {
                Debug.LogWarning($"Cannot create instance of {moduleType}. Must inherit from {_moduleInterfaceType}");
                return null;
            }

            try {
                return (IModule)Activator.CreateInstance(moduleType);
            } catch (MissingMethodException e) {
                Debug.LogWarning(e.Message);
                return null;
            }
        }
    }
}