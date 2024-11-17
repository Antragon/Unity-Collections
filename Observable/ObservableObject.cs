namespace Collections.Observable {
    using System;
    using UnityEngine;

    public class ObservableObject<T> : ScriptableObject {
        private readonly ObservableAction<T?> _onValueChanged = new();

        private T? _value;

        public T? Value {
            get => _value;
            set => SetValue(value);
        }

        public void SetValue(T? value) {
            if (Equals(_value, value)) return;
            SetAndInvoke(value);
        }

        public void SetAndInvoke(T? value) {
            _value = value;
            Invoke();
        }

        public void Invoke() {
            _onValueChanged.Invoke(_value);
        }

        public void SetSilently(T? value) {
            _value = value;
        }

        public ObservableObject<T> AddListener(Action<T?> onValueChanged) {
            _onValueChanged.AddListener(onValueChanged);
            return this;
        }

        public ObservableObject<T> AddAndInvokeListener(Action<T?> onValueChanged) {
            _onValueChanged.AddListener(onValueChanged);
            onValueChanged(Value);
            return this;
        }

        public ObservableObject<T> ListenOnce(Action<T?> onValueChanged) {
            _onValueChanged.ListenOnce(onValueChanged);
            return this;
        }

        public ObservableObject<T> RemoveListener(Action<T?> onValueChanged) {
            _onValueChanged.RemoveListener(onValueChanged);
            return this;
        }

        public override string ToString() {
            return $"<{nameof(ObservableProperty<T>)}>{Value}";
        }
    }
}