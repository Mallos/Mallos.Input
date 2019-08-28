namespace OpenInput.Mechanics
{
    using System.Linq;

    public struct InputKeys
    {
        public InputKeys(params InputKey[] keys)
        {
            this.Keys = keys;
        }

        public InputKey[] Keys { get; }

        /// <summary>
        /// Returns wether or not this contians a <see cref="InputKey" />.
        /// </summary>
        public bool HasKey(InputKey key)
        {
            return Keys.Any(e => e == key);
        }

        public override string ToString()
        {
            return string.Join(", ", this.Keys);
        }
    }
}
