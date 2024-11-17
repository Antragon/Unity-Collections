namespace Collections.Observable {
    public readonly struct ValueArgs<T> {
        public ValueArgs(T? previous, T? value) {
            Previous = previous;
            Value = value;
        }

        public T? Previous { get; }
        public T? Value { get; }

        public override string ToString() {
            return Value?.ToString() ?? string.Empty;
        }
    }
}