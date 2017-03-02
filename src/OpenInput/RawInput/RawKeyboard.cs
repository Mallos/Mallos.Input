namespace OpenInput
{
    using OpenInput.RawInput;
    using System;
    using System.Linq;
    using OpenInput.Trackers;

    /// <summary>
    /// Class that represents a Keyboard, for RawInput.
    /// </summary>
    public class RawKeyboard : RawDevice, IKeyboard
    {
        /// <inheritdoc />
        public string Name => Service.KeyboardNames;

        /// <inheritdoc />
        public event EventHandler<KeyEventArgs> KeyDown;

        /// <inheritdoc />
        public event EventHandler<KeyEventArgs> KeyUp;

        /// <inheritdoc />
        public TextInput TextInput => textInput;
        
        private TextInput textInput = new Dummy.DummyTextInput();

        // TODO: Implement TextInput

        /// <summary>
        /// Initializes a new instance of the <see cref="Keyboard"/> class.
        /// </summary>
        public RawKeyboard(IntPtr handle, bool captureInBackground = false)
            : base(handle)
        {

        }

        public IKeyboardTracker CreateTracker()
        {
            return new BasicKeyboardTracker(this);
        }

        /// <inheritdoc />
        public KeyboardState GetCurrentState()
        {
            if (Service == null)
            {
                return KeyboardState.Empty;
            }
            return new KeyboardState(Service.Keys.ToArray());
        }
    }
}
