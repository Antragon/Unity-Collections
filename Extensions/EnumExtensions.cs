namespace Collections.Extensions {
    using System;
    using System.Linq;

    public static class EnumExtensions {
        public static T[] GetValues<T>() {
            return Enum.GetValues(typeof(T)).Cast<T>().ToArray();
        }
    }
}