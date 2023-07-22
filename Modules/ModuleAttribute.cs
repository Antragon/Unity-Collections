namespace Collections.Modules {
    using System;
    using UnityEngine;

    public class ModuleAttribute : PropertyAttribute {
        public ModuleAttribute(Type moduleType) {
            ModuleType = moduleType;
        }

        public Type ModuleType { get; }
    }
}