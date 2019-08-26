namespace OpenInput.Mechanics.Input
{
    using System;

    public struct InputAction
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

        /// <summary>
        /// Gets the action name.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Gets the key.
        /// </summary>
        public InputKey Key { get; }
    }
}
