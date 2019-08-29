namespace OpenInput
{
    using OpenInput.Trackers;

    public interface IDeviceSet : ITracker
    {
        /// <summary>
        /// Gets the name of the set.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Gets the default keyboard.
        /// </summary>
        IKeyboard Keyboard { get; }

        /// <summary>
        /// Gets the default mouse.
        /// </summary>
        IMouse Mouse { get; }

        /// <summary>
        /// Gets an array of the connected gamepads.
        /// </summary>
        IGamePad[] GamePads { get; }

        /// <summary>
        /// Gets the default keyboard tracker.
        /// </summary>
        IKeyboardTracker KeyboardTracker { get; }

        /// <summary>
        /// Gets the default mouse tracker.
        /// </summary>
        IMouseTracker MouseTracker { get; }
    }
}