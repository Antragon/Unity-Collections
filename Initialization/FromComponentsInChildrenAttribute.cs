namespace Collections.Initialization {
    using System;

    public class FromComponentsInChildrenAttribute : Attribute {
        public string SingletonTag { get; set; }
    }
}