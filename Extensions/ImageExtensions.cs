namespace Collections.Extensions {
    using UnityEngine;
    using UnityEngine.UI;

    public static class ImageExtensions {
        public static void SetColorRGB(this Image spriteRenderer, float r, float g, float b) {
            var color = new Color(r, g, b);
            spriteRenderer.SetColorWithoutAlpha(color);
        }

        public static void SetColorWithoutAlpha(this Image spriteRenderer, Color color) {
            var alpha = spriteRenderer.color.a;
            spriteRenderer.color = color;
            spriteRenderer.SetColorAlpha(alpha);
        }

        public static void SetColorAlpha(this Image spriteRenderer, float alpha) {
            var color = spriteRenderer.color;
            color.a = alpha;
            spriteRenderer.color = color;
        }
    }
}