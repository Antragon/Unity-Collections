namespace Collections.Initialization {
    using System;

    public class FromComponentInChildrenAttribute : Attribute {
        public string SingletonTag { get; set; }
    }
}