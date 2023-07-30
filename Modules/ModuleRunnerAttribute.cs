namespace Collections.Modules {
    using System;

    public class ModuleRunnerAttribute : Attribute {
        public ModuleRunnerAttribute(Type moduleBaseType) {
            ModuleBaseType = moduleBaseType;
        }

        public Type ModuleBaseType { get; }
    }
}