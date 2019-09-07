namespace Mallos.Input
{
    using Mallos.Input.Trackers;

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
            this.GKeyboard = keyboard;
            this.GMouse = mouse;

            // FIXME: Figure out how to handle new gamepad connect
            this.TGamePads = gamePads;

            this.KeyboardTracker = this.GKeyboard?.CreateTracker();
            this.MouseTracker = this.GMouse?.CreateTracker();
        }

        /// <inheritdoc />
        public string Name { get; }

        /// <inheritdoc />
        public IKeyboard Keyboard => this.GKeyboard;

        /// <inheritdoc />
        public IMouse Mouse => this.GMouse;

        /// <inheritdoc />
        public IGamePad[] GamePads => this.TGamePads as IGamePad[];

        /// <inheritdoc />
        public IKeyboardTracker KeyboardTracker { get; }

        /// <inheritdoc />
        public IMouseTracker MouseTracker { get; }
        
        protected TKeyboard GKeyboard { get; }
        protected TMouse GMouse { get; }
        protected TGamePad[] TGamePads { get; }

        /// <inheritdoc />
        public void Update(float elapsedTime)
        {
            KeyboardTracker?.Update(elapsedTime);
            MouseTracker?.Update(elapsedTime);
        }
    }
}
