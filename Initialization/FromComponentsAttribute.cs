namespace Collections.Initialization {
    using System;

    public class FromComponentsAttribute : Attribute {
        public string SingletonTag { get; set; }
    }
}