namespace OpenInput
{
    using OpenInput.Touch;
    using OpenInput.Trackers;

    /// <summary>
    /// Interface for a basic device.
    /// </summary>
    public interface IDevice
    {
        /// <summary>
        /// Gets the name of the device.
        /// </summary>
        string Name { get; }
    }

    /// <summary>
    /// Interface for a device with a state.
    /// </summary>
    public interface IDevice<TTracker, TState> : IDevice 
        where TTracker : ITracker
        where TState : struct
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        TTracker CreateTracker();

        /// <summary>
        /// Gets the current state of the device.
        /// </summary>
        TState GetCurrentState();
    }
    
    /// <summary>
    /// Interface that represents a mouse.
    /// </summary>
    public interface IMouse : IDevice<IMouseTracker, MouseState>
    {
        /// <summary>
        /// Sets the mouse cursor position.
        /// </summary>
        void SetPosition(int x, int y);

        /// <summary>
        /// Gets the mouse cursor position.
        /// </summary>
        void GetPosition(out int x, out int y);
    }

    /// <summary>
    /// Interface that represents a keyboard.
    /// </summary>
    public interface IKeyboard : IDevice<IKeyboardTracker, KeyboardState>
    {
        /// <summary>
        /// Gets the <see cref="TextInput"/>.
        /// </summary>
        TextInput TextInput { get; }
    }

    /// <summary>
    /// Interface that represents a touch device.
    /// </summary>
    public interface ITouchDevice : IDevice<ITracker, TouchCollection>
    {
        /// <summary>
        /// Used to determine if a touch gesture is available.
        /// </summary>
        bool IsGestureAvailable { get; }

        /// <summary>
        /// 
        /// </summary>
        GestureSample ReadGesture();
    }

    /// <summary>
    /// Interface that represents a gamepad.
    /// </summary>
    public interface IGamePad : IDevice<IGamePadTracker, GamePadState>
    {
        /// <summary>
        /// Gets the gamepad index.
        /// </summary>
        int Index { get; }
    }
}
