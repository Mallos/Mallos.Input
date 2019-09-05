namespace Mallos.Input
{
    using System;
    using Mallos.Input.Trackers;

    public class OpenTKKeyboardTracker : IKeyboardTracker, IDisposable
    {
        public readonly OpenTKKeyboard Device;

        /// <inheritdoc />
        public event EventHandler<KeyEventArgs> KeyDown;

        /// <inheritdoc />
        public event EventHandler<KeyEventArgs> KeyUp;

        public OpenTKKeyboardTracker(OpenTKKeyboard keyboard)
        {
            this.Device = keyboard ?? throw new ArgumentNullException(nameof(keyboard));

            if (this.Device.Device == null)
                throw new ArgumentNullException(nameof(Device.Device));

            this.Device.Device.KeyDown += KeyboardDevice_KeyDown;
            this.Device.Device.KeyUp += KeyboardDevice_KeyUp;
        }

        public void Update(float elapsedTime)
        {
            // We are not using this
        }

        private void KeyboardDevice_KeyUp(object sender, OpenTK.Input.KeyboardKeyEventArgs e)
        {
            KeyUp?.Invoke(this, new KeyEventArgs(KeyboardState.Empty, Keys.A, ' '));
        }

        private void KeyboardDevice_KeyDown(object sender, OpenTK.Input.KeyboardKeyEventArgs e)
        {
            var key = OpenTKHelpers.KeyToKeys(e.Key);
            KeyDown?.Invoke(this, new KeyEventArgs(KeyboardState.Empty, key, ' '));
        }

        public void Dispose()
        {
            this.Device.Device.KeyDown -= KeyboardDevice_KeyDown;
            this.Device.Device.KeyUp -= KeyboardDevice_KeyUp;
        }
    }
}
