namespace Collections.Editor.PropertyDrawers {
    using UnityEngine;

    public class SingleLineLayout {
        private readonly Rect _rect;
        private readonly float _chunks;

        public SingleLineLayout(Rect rect, int chunks) {
            _rect = rect;
            _chunks = chunks;
        }

        public Rect Get(int start, int width) {
            var rect = _rect;
            rect.x += start / _chunks * _rect.width;
            rect.width *= width / _chunks;
            return rect;
        }
    }
}