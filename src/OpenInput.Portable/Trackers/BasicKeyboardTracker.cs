namespace OpenInput.Trackers
{
    using System;

    /// <summary>
    /// Basic keyboard tracker, takes the old and the new state then compares them.
    /// </summary>
    public class BasicKeyboardTracker : BasicDeviceTracker<IKeyboardTracker, KeyboardState>, IKeyboardTracker
    {
        /// <inheritdoc />
        public event EventHandler<KeyEventArgs> KeyDown;

        /// <inheritdoc />
        public event EventHandler<KeyEventArgs> KeyUp;

        /// <summary>
        /// Initialize a new <see cref="KeyboardTracker"/> class.
        /// </summary>
        /// <param name="keyboard"></param>
        public BasicKeyboardTracker(IKeyboard keyboard)
            : base(keyboard)
        {

        }

        /// <summary>
        /// Initialize a new <see cref="KeyboardTracker"/> class.
        /// </summary>
        /// <param name="keyboard"></param>
        public BasicKeyboardTracker(IDevice<IKeyboardTracker, KeyboardState> keyboard)
            : base(keyboard)
        {

        }

        protected override void Track(KeyboardState newState, KeyboardState oldState)
        {
            // TODO: Key to char

            var difference = newState.CompareBoth(oldState);
            if (difference.Item1.Length > 0)
            {
                foreach (var item in difference.Item1)
                {
                    KeyUp?.Invoke(this, new KeyEventArgs(newState, item, ' '));
                }
            }

            if (difference.Item2.Length > 0)
            {
                foreach (var item in difference.Item2)
                {
                    KeyDown?.Invoke(this, new KeyEventArgs(newState, item, ' '));
                }
            }
        }
    }
}
