namespace Collections.Initialization {
    using System;

    public class FromComponentAttribute : Attribute {
        public string SingletonTag { get; set; }
    }
}