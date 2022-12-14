namespace Collections {
    using System.Collections.Generic;
    using System.Linq;
    using UnityEngine.InputSystem;

    public static class ModifierKeys {
        public static readonly List<Key> Keys = new() {
            Key.LeftAlt,
            Key.LeftCtrl,
            Key.LeftShift,
            Key.RightAlt,
            Key.RightCtrl,
            Key.RightShift,
        };

        public static bool SequenceIsPressedExclusively(IEnumerable<Key> modifierKeys) {
            return GetCurrentlyPressed()
                .OrderBy(x => x)
                .SequenceEqual(modifierKeys.OrderBy(x => x));
        }

        public static IEnumerable<Key> GetCurrentlyPressed() {
            return Keys.Where(key => Keyboard.current[key].isPressed);
        }
    }
}