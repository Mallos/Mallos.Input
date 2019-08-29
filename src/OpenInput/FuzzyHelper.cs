namespace OpenInput
{
    using System;

    public static class FuzzyHelper
    {
        public static int StartWith<T>(T[] value1, T[] value2)
            where T : IEquatable<T>
        {
            if (value1.Length > value2.Length)
            {
                return value1.Length - value2.Length;
            }

            int matchFuzzy = 0;
            for (int i = 0; i < value1.Length; i++)
            {
                if (!value1[i].Equals(value2[i]))
                {
                    matchFuzzy += 1;
                }
            }

            return matchFuzzy;
        }
    }
}
