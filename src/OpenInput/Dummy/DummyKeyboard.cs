namespace OpenInput.Dummy
{
    using OpenInput.Trackers;

    /// <summary>
    /// Dummy Keyboard, does nothing.
    /// </summary>
    public sealed class DummyKeyboard : IKeyboard
    {
        /// <inheritdoc />
        public string Name => "Dummy Keyboard";

        /// <inheritdoc />
        public IKeyboardTracker CreateTracker() => new BasicKeyboardTracker(this);

        /// <inheritdoc />
        public TextInput TextInput { get; } = new DummyTextInput();

        /// <inheritdoc />
        public KeyboardState GetCurrentState() => KeyboardState.Empty;
    }
}
