namespace Collections.Ranges {
    public interface IRange<out T> {
        T Start { get; }
        T End { get; }
    }
}