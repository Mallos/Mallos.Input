namespace OpenInput.Mechanics.Combo
{
    using System.Linq;

    public struct SequenceCombo
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

        public override string ToString()
        {
            var keys = Keys.Select(e => e.ToString());
            return $"{this.Name}: {string.Join(",", keys)}";
        }
    }
}
