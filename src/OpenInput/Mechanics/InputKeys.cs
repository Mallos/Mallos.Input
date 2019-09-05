namespace OpenInput.Mechanics
{
    using System.Linq;

    public struct InputKeys
    {
        /// <summary>
        /// Initializes a new instance of <see cref="InputKeys"/>.
        /// </summary>
        public InputKeys(params InputKey[] keys)
        {
            this.Keys = keys;
        }

        /// <summary>
        /// Gets the keys.
        /// </summary>
        public InputKey[] Keys { get; }

        /// <summary>
        /// Returns wether or not this contians a <see cref="InputKey" />.
        /// </summary>
        public bool HasKey(InputKey key) => this.Keys.Any(e => e == key);

        public override string ToString() => string.Join(", ", this.Keys);
    }
}
