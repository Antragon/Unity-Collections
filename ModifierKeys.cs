namespace Collections {
    using System.Collections.Generic;
    using System.Linq;
    using UnityEngine;

    public static class ModifierKeys {
        public static readonly List<KeyCode> Keys = new() {
            KeyCode.LeftAlt,
            KeyCode.LeftControl,
            KeyCode.LeftShift,
            KeyCode.RightAlt,
            KeyCode.RightControl,
            KeyCode.RightShift,
        };

        public static bool SequenceIsPressedExclusively(IEnumerable<KeyCode> modifierKeys) {
            return GetCurrentlyPressed()
                .OrderBy(x => x)
                .SequenceEqual(modifierKeys.OrderBy(x => x));
        }

        public static IEnumerable<KeyCode> GetCurrentlyPressed() {
            return Keys.Where(Input.GetKey);
        }
    }
}