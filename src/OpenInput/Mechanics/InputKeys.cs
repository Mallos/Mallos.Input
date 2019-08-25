namespace OpenInput.Mechanics
{
    using System.Linq;

    public struct InputKeys
    {
        public InputKeys(params Keys[] keys)
        {
            this.Keys = keys.Select(key => new InputKey(key)).ToArray();
        }

        public InputKeys(params Buttons[] keys)
        {
            this.Keys = keys.Select(key => new InputKey(key)).ToArray();
        }

        public InputKey[] Keys { get; }

        /// <summary>
        /// Returns wether or not this contians a <see cref="InputKey" />.
        /// </summary>
        public bool HasKey(InputKey key)
        {
            return Keys.Any(e => e == key);
        }
    }
}
