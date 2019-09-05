namespace Mallos.Input.Mechanics
{
    using System;
    using System.Linq;

    public enum InputKeyMatch
    {
        NoMatch,
        PartialMatch,
        FullMatch,
    }

    // FIXME: Come up with a better way to index it.
    public class InputKeyIndex
    {
        private readonly int[] index;

        public InputKeyIndex(InputKey[] keys)
        {
            this.index = this.CreateIndex(keys);
        }

        public bool Match(InputKeyIndex other, bool partial = false)
        {
            if (partial)
            {
                var otherSpan = new Span<int>(other.index, 0, this.index.Length);
                return this.index.Length >= other.index.Length &&
                       otherSpan.StartsWith(this.index);
            }
            else
            {
                return this.index.Length == other.index.Length &&
                       Enumerable.SequenceEqual(this.index, other.index);
            }
        }

        public InputKeyMatch FuzzyMatch(InputKeyIndex other, int fuzzy = 0)
        {
            if (this.Match(other))
            {
                return InputKeyMatch.FullMatch;
            }

            // We don't want a lot of shorter versions that might match.
            // This will just flood our results otherwise.
            if (this.index.Length < other.index.Length)
            {
                return InputKeyMatch.NoMatch;
            }

            var startWithResult = FuzzyHelper.StartWith(this.index, other.index);
            if (startWithResult <= fuzzy)
            {
                return InputKeyMatch.PartialMatch;
            }

            return InputKeyMatch.NoMatch;
        }

        private int[] CreateIndex(InputKey[] keys)
        {
            var result = new int[keys.Length];

            for (var i = 0; i < keys.Length; i++)
            {
                var type = (int)keys[i].Type + 1;
                var attr = keys[i].ButtonAsInteger() + 1;
                result[i + 0] = type * attr;
            }

            return result;
        }
    }
}
