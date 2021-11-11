namespace Collections.Extensions {
    using UnityEngine;

    public static class PhysicsExtensions {
        public static ContactFilter2D GetContactFilter2D(this GameObject gameObject) {
            var contactFilter = new ContactFilter2D { useTriggers = true };
            var layerMask = Physics2D.GetLayerCollisionMask(gameObject.layer);
            contactFilter.SetLayerMask(layerMask);
            return contactFilter;
        }
    }
}