namespace OpenInput
{
    using System;
    using System.ComponentModel;

    [EditorBrowsable(EditorBrowsableState.Never)]
    public static class FuzzyHelper
    {
        public static int StartWith<T>(T[] value1, T[] value2)
            where T : IEquatable<T>
        {
            int matchFuzzy = 0;
            for (int i = 0; i < value1.Length; i++)
            {
                if (i >= value2.Length || !value1[i].Equals(value2[i]))
                {
                    matchFuzzy += 1;
                }
            }

            return matchFuzzy;
        }
    }
}
