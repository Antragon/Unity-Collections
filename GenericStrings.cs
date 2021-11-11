namespace Collections {
    using UnityEngine;

    public static class GenericStrings {
        public static string ThreeDots() {
            var count = (int) (Time.time % 3) + 1;
            return $"{new string('.', count)}{new string('\u00A0', 3 - count)}";
        }
    }
}