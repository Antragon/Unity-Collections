namespace Collections.Components {
    using UnityEngine;

    public class InstanceInfo : MonoBehaviour {
        public InstanceFactory Creator { get; internal set; } = null!;
    }
}