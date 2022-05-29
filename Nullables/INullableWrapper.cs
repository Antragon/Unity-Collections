namespace Collections.Nullables {
    public interface INullableWrapper<out T> {
        T Value { get; }
        bool HasValue { get; }
    }
}