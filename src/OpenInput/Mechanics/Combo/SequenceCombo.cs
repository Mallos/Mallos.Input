namespace OpenInput.Mechanics.Combo
{
    using System;
    using System.Linq;
    using System.Collections.Generic;

    public struct SequenceCombo : IEquatable<SequenceCombo>
    {
        internal readonly InputKeyIndex Index;

        public SequenceCombo(string name, params InputKey[] keys)
        {
            this.Name = name;
            this.Keys = keys;
            this.Index = new InputKeyIndex(keys);
        }

        /// <summary>
        /// Gets the name of the sequence.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Gets the keys that are required to trigger it.
        /// </summary>
        public InputKey[] Keys { get; }

        /// <summary>
        /// Check wether the keys match or not.
        /// </summary>
        public bool KeysMatch(IEnumerable<InputKey> keys)
        {
            return this.Keys.Length == keys.Count() &&
                   Enumerable.SequenceEqual(this.Keys, keys);
        }

        public bool Equals(SequenceCombo other)
        {
            return this.Name == other.Name && this.KeysMatch(other.Keys);
        }

        public override string ToString()
        {
            if (this.Keys != null)
            {
                var keys = Keys.Select(e => e.ToString());
                return $"{this.Name}: {string.Join(",", keys)}";
            }
            return $"{this.Name}: NaN";
        }
    }
}
