namespace OpenInput.Trackers
{
    using System;

    /// <summary>
    /// Basic keyboard tracker, takes the old and the new state then compares them.
    /// </summary>
    public class BasicKeyboardTracker : BasicDeviceTracker<IKeyboardTracker, KeyboardState>, IKeyboardTracker
    {
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

        /// <inheritdoc />
        public event EventHandler<KeyEventArgs> KeyDown;

        /// <inheritdoc />
        public event EventHandler<KeyEventArgs> KeyUp;

        protected override void Track(KeyboardState newState, KeyboardState oldState)
        {
            // TODO: Key to char

            Tuple<Keys[], Keys[]> difference = newState.CompareBoth(oldState);
            if (difference.Item1.Length > 0)
            {
                foreach (Keys item in difference.Item1)
                {
                    this.KeyUp?.Invoke(this, new KeyEventArgs(newState, item, ' '));
                }
            }

            if (difference.Item2.Length > 0)
            {
                foreach (Keys item in difference.Item2)
                {
                    this.KeyDown?.Invoke(this, new KeyEventArgs(newState, item, ' '));
                }
            }
        }
    }
}
