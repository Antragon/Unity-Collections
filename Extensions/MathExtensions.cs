namespace Collections.Extensions {
    using UnityEngine;

    public static class MathExtensions {
        public static float RoundToDecimals(this float value, int decimals) {
            var multiplier = Mathf.Pow(10, decimals);
            return value.RoundByMultiplier(multiplier);
        }

        public static float RoundByMultiplier(this float value, float multiplier) {
            var roundedValue = Mathf.Round(value * multiplier) / multiplier;
            return roundedValue;
        }
        
        public static float RoundBySteps(this float value, float steps) {
            var roundedValue = Mathf.Round(value / steps) * steps;
            return roundedValue;
        }
    }
}