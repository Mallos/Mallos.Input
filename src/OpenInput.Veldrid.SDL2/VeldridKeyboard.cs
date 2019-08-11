namespace OpenInput
{
    using OpenInput.Trackers;
    using System.Collections.Generic;
    using System.Linq;
    using Veldrid;

    public class VeldridKeyboard : VeldridDevice, IKeyboard
    {
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
            var pressedKeys = GetPressedKeys(snapshot.KeyEvents);
            this.currentState = new KeyboardState(pressedKeys);
        }

        private Keys[] GetPressedKeys(IReadOnlyList<KeyEvent> keyEvents)
        {
            var pressedKeys = keyEvents.Where(key => key.Down).ToArray();
            if (pressedKeys.Length == 0)
            {
                return new Keys[0];
            }

            var result = new Keys[pressedKeys.Length];
            for (int i = 0; i < result.Length; i++)
            {
                result[i] = pressedKeys[i].Key.ConvertKey();
            }

            return result;
        }
    }
}
