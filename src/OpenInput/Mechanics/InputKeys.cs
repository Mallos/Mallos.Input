namespace OpenInput.Mechanics
{
    using System.Linq;

    public struct InputKeys
    {
        public InputKey[] Keys { get; }

        public InputKeys(params Keys[] keys)
        {
            this.Keys = keys.Select(key => new InputKey(key)).ToArray();
        }

        public InputKeys(params Buttons[] keys)
        {
            this.Keys = keys.Select(key => new InputKey(key)).ToArray();
        }
    }
}
