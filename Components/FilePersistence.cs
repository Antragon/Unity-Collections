﻿namespace Collections.Components {
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using Collections;
    using UnityEngine;

    public class FilePersistence : MonoBehaviour {
        private readonly Dictionary<string, object> _toBeSaved = new();
        private readonly HashSet<string> _currentlyRunning = new();

        private CancellationTokenSource _cancellationTokenSource;

        private void OnEnable() {
            _cancellationTokenSource = new CancellationTokenSource();
        }

        private void OnDestroy() {
            _cancellationTokenSource.Cancel();
            _cancellationTokenSource = null;
        }

        public void Save(string filePath, object @object) {
            _toBeSaved[filePath] = @object;
            if (_currentlyRunning.Add(filePath)) {
                Task.Run(() => SaveAsync(filePath));
            }
        }

        private async Task SaveAsync(string filePath) {
            while (_toBeSaved.Remove(filePath, out var toBeSaved)) {
                await DataSaver.SaveJsonAsync(filePath, toBeSaved, _cancellationTokenSource.Token);
            }

            _currentlyRunning.Remove(filePath);
        }
    }
}