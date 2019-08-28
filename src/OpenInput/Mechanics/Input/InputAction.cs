namespace OpenInput.Mechanics.Input
{
    using System;

    public struct InputAction : IInputTrigger
    {
        /// <summary>
        /// Initialize a new <see cref="InputAction"/> structure.
        /// </summary>
        public InputAction(string name, InputKey key)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException(nameof(name));
            }

            this.Name = name;
            this.Key = key;
        }

        /// <inheritdoc />
        public string Name { get; }

        /// <inheritdoc />
        public InputKey Key { get; }
    }
}
