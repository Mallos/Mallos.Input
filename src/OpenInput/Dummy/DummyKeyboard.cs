namespace OpenInput.Dummy
{
    using OpenInput.Trackers;

    public class DummyTextInput : TextInput
    {
    }

    /// <summary>
    /// Dummy Keyboard, does nothing.
    /// </summary>
    public sealed class DummyKeyboard : IKeyboard
    {
        /// <inheritdoc />
        public string Name => "Dummy Keyboard";

        /// <inheritdoc />
        public IKeyboardTracker CreateTracker()
        {
            return new BasicKeyboardTracker(this);
        }

        /// <inheritdoc />
        public TextInput TextInput => textInput;

        private readonly TextInput textInput = new DummyTextInput();

        /// <inheritdoc />
        public KeyboardState GetCurrentState()
        {
            return KeyboardState.Empty;
        }
    }
}
