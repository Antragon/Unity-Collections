namespace Collections.Extensions {
    using UnityEngine;

    public static class SpriteRendererExtensions {
        public static void SetSpriteColorRGB(this SpriteRenderer spriteRenderer, float r, float g, float b) {
            var color = new Color(r, g, b);
            spriteRenderer.SetSpriteColorWithoutAlpha(color);
        }

        public static void SetSpriteColorWithoutAlpha(this SpriteRenderer spriteRenderer, Color color) {
            var alpha = spriteRenderer.color.a;
            spriteRenderer.color = color;
            spriteRenderer.SetSpriteColorAlpha(alpha);
        }

        public static void SetSpriteColorAlpha(this SpriteRenderer spriteRenderer, float alpha) {
            var color = spriteRenderer.color;
            color.a = alpha;
            spriteRenderer.color = color;
        }
    }
}