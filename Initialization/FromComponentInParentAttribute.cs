namespace Collections.Initialization {
    using System;

    public class FromComponentInParentAttribute : Attribute {
        public string SingletonTag { get; set; }
    }
}