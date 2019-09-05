namespace OpenInput
{
    using System;

    /// <summary>
    /// Keyboard event args, for when a key is pressed.
    /// </summary>
    public class KeyEventArgs : EventArgs
    {
        /// <summary>
        /// Initializes a new instance of <see cref="KeyEventArgs"/>.
        /// </summary>
        public KeyEventArgs(KeyboardState state, Keys key, char keyChar)
        {
            this.Key = key;
            this.State = state;
            this.KeyChar = keyChar;
        }

        /// <summary>
        /// Gets the pressed key.
        /// </summary>
        public Keys Key { get; }

        /// <summary>
        /// Gets the char of the key.
        /// </summary>
        public char KeyChar { get; }

        /// <summary>
        /// Gets the state as of when the event occurs.
        /// </summary>
        public KeyboardState State { get; }
    }
}
