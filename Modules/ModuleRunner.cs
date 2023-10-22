namespace Collections.Modules {
    using System.Collections.Generic;
    using System.Linq;
    using Initialization;
    using MoreLinq;
    using UnityEngine;
#if UNITY_EDITOR
    using UnityEditor;
#endif

    public abstract class ModuleRunner : MonoBehaviour {
#if UNITY_EDITOR
        protected void OnValidate() {
            _moduleBuilders?.ForEach(m => m.UpdateType());
            EditorUtility.SetDirty(this);
        }
#endif

        private readonly List<IModule> _modules = new();

        [SerializeField] private ModuleBuilder[] _moduleBuilders;

        protected void Awake() {
            this.Initialize();
            _moduleBuilders
                .Select(m => m.CreateModule())
                .Where(m => m != null)
                .ForEach(InitializeModule);
        }

        private void InitializeModule(IModule module) {
            module.Initialize(this);
            module.CoroutineCallback.AddListener(coroutine => StartCoroutine(coroutine));
            _modules.Add(module);
        }

        protected virtual void Start() {
            _modules.ForEach(m => m.Start());
        }

        protected virtual void Update() {
            _modules.ForEach(m => m.Update());
        }
    }
}