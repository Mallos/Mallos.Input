namespace OpenInput.Empty
{
    using System;

    /// <summary>
    /// Empty Keyboard, does nothing.
    /// </summary>
    public sealed class Keyboard : IKeyboard
    {
        /// <inheritdoc />
        public string Name => "Empty Keyboard";

        /// <inheritdoc />
        public event EventHandler<KeyEventArgs> KeyDown;

        /// <inheritdoc />
        public event EventHandler<KeyEventArgs> KeyUp;

        /// <inheritdoc />
        public TextInput TextInput => textInput;
        private TextInput textInput = new EmptyTextInput();

        /// <inheritdoc />
        public KeyboardState GetCurrentState()
        {
            return new KeyboardState();
        }
    }
}
