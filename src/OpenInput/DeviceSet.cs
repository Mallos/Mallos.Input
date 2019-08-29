namespace OpenInput
{
    using OpenInput.Trackers;

    /// <summary>
    /// DeviceSet is a set of all devices that allows for easier usage of the library.
    /// </summary>
    public class DeviceSet<TKeyboard, TMouse, TGamePad> : IDeviceSet
        where TKeyboard : IKeyboard
        where TMouse : IMouse
        where TGamePad : IGamePad
    {
        public DeviceSet(
            string name,
            TKeyboard keyboard,
            TMouse mouse,
            TGamePad[] gamePads)
        {
            this.Name = name;
            this.Keyboard = keyboard;
            this.Mouse = mouse;

            // FIXME: Figure out how to handle new gamepad connect
            this.GamePads = gamePads;

            this.KeyboardTracker = Keyboard?.CreateTracker();
            this.MouseTracker = Mouse?.CreateTracker();
        }

        /// <inheritdoc />
        public string Name { get; }

        protected TKeyboard Keyboard { get; }
        protected TMouse Mouse { get; }
        protected TGamePad[] GamePads { get; }

        /// <inheritdoc />
        IKeyboard IDeviceSet.Keyboard => this.Keyboard;

        /// <inheritdoc />
        IMouse IDeviceSet.Mouse => this.Mouse;

        /// <inheritdoc />
        IGamePad[] IDeviceSet.GamePads => this.GamePads as IGamePad[];

        /// <inheritdoc />
        public IKeyboardTracker KeyboardTracker { get; }

        /// <inheritdoc />
        public IMouseTracker MouseTracker { get; }

        /// <inheritdoc />
        public void Update(float elapsedTime)
        {
            KeyboardTracker?.Update(elapsedTime);
            MouseTracker?.Update(elapsedTime);
        }
    }
}
