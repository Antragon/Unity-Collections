namespace Collections.Extensions {
    using UnityEngine;

    public static class RendererExtensions {
        public static void Enable(this Renderer renderer, bool value) {
            renderer.enabled = value;
        }
    }
}