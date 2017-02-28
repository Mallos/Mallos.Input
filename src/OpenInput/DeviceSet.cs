namespace OpenInput
{
    using System;
    using OpenInput.Trackers;

    /// <summary>
    /// DeviceSet is a set of all devices that allows for easier usage of the library.
    /// </summary>
    public class DeviceSet : ITracker
    {
        /// <summary>
        /// Gets the name of the set.
        /// </summary>
        public readonly string Name;

        // Points towards the device itself.
        public readonly IKeyboard Keyboard;
        public readonly IMouse Mouse;
        public readonly IGamePad[] GamePads;

        // A tracker allows you to create event driven input. 
        // NOTE: Update has to be called on some types.
        public readonly IKeyboardTracker KeyboardTracker;
        public readonly IMouseTracker MouseTracker;

        public DeviceSet(string name, IKeyboard keyboard, IMouse mouse, Func<int, IGamePad> createGamePads)
        {
            this.Name = name;
            this.Keyboard = keyboard;
            this.Mouse = mouse;

            if (this.Keyboard != null)
                this.KeyboardTracker = Keyboard.CreateTracker();

            if (this.Mouse != null)
                this.MouseTracker = Mouse.CreateTracker();

            this.GamePads = new IGamePad[4];
            for (int i = 0; i < 4; i++)
            {
                this.GamePads[i] = createGamePads(i);
            }
        }

        public void Update(float elapsedTime)
        {
            KeyboardTracker?.Update(elapsedTime);
            MouseTracker?.Update(elapsedTime);
        }
    }
}
