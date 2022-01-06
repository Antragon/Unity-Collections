namespace Collections.Components {
    using System;
    using UnityEngine;

#line hidden
    public class InfoLogger : SingletonComponent<InfoLogger> {
        public void WriteInfo(string message) {
            Debug.Log(message);
        }

        public void WriteWarning(string message) {
            Debug.LogWarning(message);
        }

        public void WriteError(string message, Exception exception = null) {
            Debug.LogError(message);
            if (exception != null) {
                Debug.LogError(exception.Message);
            }
        }
    }
#line default
}