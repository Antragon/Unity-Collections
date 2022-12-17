namespace Collections.Components {
    using UnityEngine;
    using UnityEngine.UI;

    /// <summary>
    /// Mouse Events need any type of raycast target. A transparent image will do, but this should create less overhead
    /// </summary>
    [RequireComponent(typeof(CanvasRenderer))]
    public class InteractableUI : Graphic {
        protected override void Awake() {
            base.Awake();
            color = Color.clear;
        }
    }
}