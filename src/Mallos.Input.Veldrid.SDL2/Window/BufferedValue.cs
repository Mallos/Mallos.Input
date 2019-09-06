namespace Mallos.Input.Window
{
    using System.Diagnostics;
    using System.Threading;

    [DebuggerDisplay("{DebuggerDisplayString,nq}")]
    internal class BufferedValue<T> where T : struct
    {
        public T Value
        {
            get => Current.Value;
            set
            {
                Back.Value = value;
                Back = Interlocked.Exchange(ref Current, Back);
            }
        }

        private ValueHolder Current = new ValueHolder();
        private ValueHolder Back = new ValueHolder();

        public static implicit operator T(BufferedValue<T> bv) => bv.Value;

        private string DebuggerDisplayString => $"{Current.Value}";

        private class ValueHolder
        {
            public T Value;
        }
    }
}
