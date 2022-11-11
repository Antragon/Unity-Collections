namespace Collections.Initialization {
    using System;

    public class FromComponentsInParentAttribute : Attribute {
        public string SingletonTag { get; set; }
    }
}