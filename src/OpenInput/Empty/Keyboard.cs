using System;

namespace OpenInput.Empty
{
    /// <summary>
    /// Empty Keyboard, does nothing.
    /// </summary>
    public sealed class Keyboard : IKeyboard
    {
        /// <inheritdoc />
        public string Name => "Empty Keyboard";

        /// <inheritdoc />
        public OpenInput.TextInput TextInput => textInput;
        private TextInput textInput = new TextInput();

        /// <inheritdoc />
        public KeyboardState GetCurrentState()
        {
            return new KeyboardState();
        }
    }
}
