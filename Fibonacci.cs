namespace Collections {
    using System.Collections.Generic;

    public static class Fibonacci {
        private static readonly List<int> _fibonacciList = new() { 1, 2, 3 };

        public static int GetNumber(int index) {
            while (_fibonacciList.Count <= index) {
                _fibonacciList.Add(_fibonacciList[index - 1] + _fibonacciList[index - 2]);
            }

            return _fibonacciList[index];
        }
    }
}