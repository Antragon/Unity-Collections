namespace Collections.Observable {
    using System;
    using System.Collections.Generic;
    using Newtonsoft.Json;

    [Serializable]
    public class ObservableProperty<T> : IObservableValue<T>, IObservationValueExtender<T> {
        private readonly List<Action<ValueArgs<T>>> _oneTimeListeners = new();

        private T? _value;

        [JsonIgnore]
        public ObservableValue<T> ReadOnly { get; }

        public ObservableProperty(T? value = default) {
            Value = value;
            ReadOnly = new ObservableValue<T>(this);
        }

        private event Action<ValueArgs<T>>? Event;

        public T? Value {
            get => _value;
            set => SetValue(value);
        }

        public void SetValue(T? value) {
            if (Equals(_value, value)) return;
            SetAndInvoke(value);
        }

        public void SetAndInvoke(T? value) {
            var valueChange = new ValueArgs<T>(_value, value);
            _value = value;
            Event?.Invoke(valueChange);
        }

        public void Invoke() {
            var valueChange = new ValueArgs<T>(Value, Value);
            Event?.Invoke(valueChange);
            foreach (var action in _oneTimeListeners.ToArray()) {
                Event -= action;
                _oneTimeListeners.Remove(action);
            }
        }

        public void SetSilently(T? value) {
            _value = value;
        }

        public ObservableValueCallback<T> AddAndInvokeListener(Action<T?> action) {
            return AddAndInvokeChangeListener(args => action(args.Value));
        }

        public ObservableValueCallback<T> AddAndInvokeChangeListener(Action<ValueArgs<T>> action) {
            var valueChange = new ValueArgs<T>(Value, Value);
            action(valueChange);
            return AddChangeListener(action);
        }

        public ObservableValueCallback<T> AddListener(Action<T?> action) {
            return AddChangeListener(args => action(args.Value));
        }

        public ObservableValueCallback<T> AddChangeListener(Action<ValueArgs<T>> action) {
            return new ObservableValueCallback<T>(this).AddChangeListener(action);
        }

        public ObservableValueCallback<T> ListenOnce(Action<T?> action) {
            return ListenToChangeOnce(args => action(args.Value));
        }

        public ObservableValueCallback<T> ListenToChangeOnce(Action<ValueArgs<T>> action) {
            return new ObservableValueCallback<T>(this).ListenToChangeOnce(action);
        }

        void IObservationValueExtender<T>.AddListener(Action<ValueArgs<T>> action) {
            Event += action;
        }

        void IObservationValueExtender<T>.ListenOnce(Action<ValueArgs<T>> action) {
            Event += action;
            _oneTimeListeners.Add(action);
        }

        void IObservationValueExtender<T>.RemoveListener(Action<ValueArgs<T>> action) {
            Event -= action;
            _oneTimeListeners.Remove(action);
        }

        public override string ToString() {
            return $"<{nameof(ObservableProperty<T>)}>{Value}";
        }
    }
}