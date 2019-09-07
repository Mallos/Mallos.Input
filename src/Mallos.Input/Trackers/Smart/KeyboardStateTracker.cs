namespace Mallos.Input.Trackers.Smart
{
    using System;
    using System.Collections.Concurrent;
    using System.Linq;

    public class KeyboardStateTracker : IKeyboardTracker
    {
        private readonly ConcurrentDictionary<Keys, bool> keys =
            new ConcurrentDictionary<Keys, bool>();

        /// <summary>
        /// Gets the current device state.
        /// </summary>
        public KeyboardState KeyboardState { get; private set; }

        /// <inheritdoc />
        public event EventHandler<KeyEventArgs> KeyDown;

        /// <inheritdoc />
        public event EventHandler<KeyEventArgs> KeyUp;

        public void Update(float elapsedTime)
        {
        }

        public void OnKeyDown(Keys key, char keyChar)
        {
            this.keys.TryAdd(key, true);
            this.KeyboardState = new KeyboardState(this.keys.Keys.ToArray());

            this.KeyDown?.Invoke(this, new KeyEventArgs(this.KeyboardState, key, keyChar));
        }

        public void OnKeyUp(Keys key, char keyChar)
        {
            this.keys.TryRemove(key, out _);
            this.KeyboardState = new KeyboardState(this.keys.Keys.ToArray());

            this.KeyUp?.Invoke(this, new KeyEventArgs(this.KeyboardState, key, keyChar));
        }
    }
}
