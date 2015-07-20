namespace OpenInput.RawInput
{
    using System;
    using System.Linq;

    /// <summary>
    /// Class that represents a Keyboard, for RawInput.
    /// </summary>
    public class Keyboard : RawDevice, IKeyboard
    {
        /// <inheritdoc />
        public string Name => Service.KeyboardNames;

        /// <inheritdoc />
        public event EventHandler<KeyEventArgs> KeyDown;

        /// <inheritdoc />
        public event EventHandler<KeyEventArgs> KeyUp;

        /// <inheritdoc />
        public TextInput TextInput => textInput;
        private TextInput textInput = new Empty.EmptyTextInput();

        // TODO: Implement TextInput

        /// <summary>
        /// Initializes a new instance of the <see cref="Keyboard"/> class.
        /// </summary>
        public Keyboard(IntPtr handle, bool captureInBackground = false)
            : base(handle)
        {

        }

        /// <inheritdoc />
        public KeyboardState GetCurrentState()
        {
            return new KeyboardState(Service.Keys.ToArray());
        }
    }
}
