namespace Collections.Initialization {
    using System;

    public class FromComponentInSingletonsAttribute : Attribute {
        public FromComponentInSingletonsAttribute(string tag = null) {
            Tag = tag;
        }

        public string Tag { get; }
    }
}