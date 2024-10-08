﻿namespace Collections {
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using Debug = UnityEngine.Debug;

    public static class Analyzer {
        private static readonly Dictionary<string, Stopwatch> _measurements = new();

        public static T ExecuteAndMeasureTime<T>(Func<T> funcToExecute, long debugLimitInMilliseconds = 1000) {
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            var result = funcToExecute();
            stopwatch.Stop();
            var elapsed = stopwatch.ElapsedMilliseconds;
            if (elapsed > debugLimitInMilliseconds) {
                Debug.Log($"Took {elapsed} ms: Executing {funcToExecute.Method.Name}");
            }

            return result;
        }

        public static void ExecuteAndMeasureTime(Action actionToExecute, long debugLimitInMilliseconds = 1000) {
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            actionToExecute();
            stopwatch.Stop();
            var elapsed = stopwatch.ElapsedMilliseconds;
            if (elapsed > debugLimitInMilliseconds) {
                Debug.Log($"Took {elapsed} ms: Executing {actionToExecute.Method.Name}");
            }
        }

        public static void StartMeasuring(string key) {
            var stopwatch = new Stopwatch();
            _measurements[key] = stopwatch;
            stopwatch.Start();
        }

        public static void StopMeasuring(string key, string activityDescription, long debugLimitInMilliseconds = 1000) {
            var stopwatch = _measurements[key];
            stopwatch.Stop();
            var elapsed = stopwatch.ElapsedMilliseconds;
            if (elapsed > debugLimitInMilliseconds) {
                Debug.Log($"Took {elapsed} ms: {activityDescription}");
            }
        }
    }
}