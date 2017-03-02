namespace OpenInput
{
    using OpenInput.Trackers;
    using OpenTK.Input;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// OpenTK Keyboard.
    /// </summary>
    public class OpenTKKeyboard : OpenTKDevice<KeyboardDevice>, IKeyboard, IDisposable
    {
        /// <inheritdoc />
        public string Name => HasDevice ? Device.Description : "OpenTK Keyboard";

        /// <inheritdoc />
        public TextInput TextInput => textInput;
        private TextInput textInput = new Dummy.DummyTextInput();

        private List<Keys> keys = new List<Keys>();

        /// <summary>
        /// Initializes a new instance of the <see cref="OpenTKKeyboard"/> class.
        /// </summary>
        public OpenTKKeyboard(KeyboardDevice keyboardDevice = null)
            : base(keyboardDevice)
        {
            if (HasDevice)
            {
                this.Device.KeyDown += KeyboardDevice_KeyDown;
                this.Device.KeyUp += KeyboardDevice_KeyUp;
            }
        }

        /// <inheritdoc />
        public KeyboardState GetCurrentState()
        {
            if (HasDevice)
            {
                return new KeyboardState(keys.ToArray());
            }
            else
            {
                //OpenTK.Input.Keyboard
                throw new NotImplementedException();
            }
        }

        /// <inheritdoc />
        public IKeyboardTracker CreateTracker()
        {
            return HasDevice ?
                new OpenTKKeyboardTracker(this) :
                (IKeyboardTracker)new BasicKeyboardTracker(this);
        }

        private void KeyboardDevice_KeyDown(object sender, KeyboardKeyEventArgs e)
        {
            var key = OpenTKHelpers.KeyToKeys(e.Key);
            keys.Add(key);
        }

        private void KeyboardDevice_KeyUp(object sender, KeyboardKeyEventArgs e)
        {
            var key = OpenTKHelpers.KeyToKeys(e.Key);
            keys.Remove(key);
        }

        public void Dispose()
        {
            if (HasDevice)
            {
                this.Device.KeyDown -= KeyboardDevice_KeyDown;
                this.Device.KeyUp -= KeyboardDevice_KeyUp;
            }
        }
    }
}
