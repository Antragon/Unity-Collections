namespace Collections.Components {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using UnityEngine;

    public class ActionQueue : MonoBehaviour {
        private readonly SortedDictionary<int, Action> _queue = new();

        [SerializeField, Range(1, 100)] private int _actionsPerFrame;

        private int _current = -1;

        private void OnEnable() {
            StartCoroutine(ProcessQueue());
        }

        private IEnumerator ProcessQueue() {
            while (enabled) {
                for (var i = 0; i < _actionsPerFrame; i++) {
                    if (_queue.Count == 0) break;
                    ProcessNext();
                }

                yield return null;
            }
        }

        private void ProcessNext() {
            var entry = _queue.FirstOrDefault();
            entry.Value.Invoke();
            _queue.Remove(entry.Key);
        }

        public ActionCallback Add(Action action) {
            var token = _current++;
            _queue.Add(token, action);
            var callback = new ActionCallback(() => _queue.Remove(token));
            return callback;
        }
    }
}