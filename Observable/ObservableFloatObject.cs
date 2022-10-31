namespace Collections.Observable {
    using UnityEngine;

    [CreateAssetMenu(fileName = nameof(ObservableFloatObject), menuName = nameof(ScriptableObject) + "/" + nameof(ObservableFloatObject), order = 0)]
    public class ObservableFloatObject : ObservableObject<float> { }
}