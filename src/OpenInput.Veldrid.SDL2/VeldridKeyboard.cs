namespace OpenInput
{
    using System.Collections.Generic;
    using System.Linq;
    using OpenInput.Trackers;
    using Veldrid;

    public class VeldridKeyboard : VeldridDevice, IKeyboard
    {
        private readonly HashSet<Key> pressedKeys = new HashSet<Key>();
        private KeyboardState currentState = new KeyboardState();

        /// <inheritdoc />
        public TextInput TextInput => throw new System.NotImplementedException();

        /// <inheritdoc />
        public string Name => "Veldrid Keyboard";

        /// <inheritdoc />
        public IKeyboardTracker CreateTracker() => new BasicKeyboardTracker(this);

        /// <inheritdoc />
        public KeyboardState GetCurrentState() => this.currentState;

        internal override void UpdateSnapshot(InputSnapshot snapshot)
        {
            foreach (KeyEvent key in snapshot.KeyEvents)
            {
                if (key.Down)
                {
                    this.pressedKeys.Add(key.Key);
                }
                else
                {
                    this.pressedKeys.Remove(key.Key);
                }
            }

            Keys[] newKeys = this.pressedKeys.Select(key => key.ConvertKey()).ToArray();
            this.currentState = new KeyboardState(newKeys);
        }
    }
}
