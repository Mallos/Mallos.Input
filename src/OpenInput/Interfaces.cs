namespace OpenInput
{
    using System;

    // TODO: Write documentation over all the devices 'Keyboard, mouse, gamepad etc' for standard outputs, 
    //       that should match every platform. Standardization.

    // TODO: I have to make some changes so I can support multiple devices of one type
    //       For example I should support multiple GamePads
    //       with RawInput I can also support multiple mices and keyboards; currently it is all in one.
    //       I also want to support functions like accessing the capabilities of the devices 
    //       and how many are connected. Maybe even events when a new device is connected and disconnected!

    // TODO: Making it all thread-safe would also be a nice thing, but that is more a platform thing.

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
    public interface IDevice<TState> : IDevice 
        where TState : struct
    {
        /// <summary>
        /// Gets the current state of the device.
        /// </summary>
        TState GetCurrentState();
    }

    // TODO: Change IDevices to something more boundle interface

    /// <summary>
    /// Interface for multiple devices.
    /// </summary>
    public interface IDevices<TDevice, TState> 
        where TDevice : IDevice<TState>
        where TState : struct
    {
        /// <summary> Occurs when a new device is connecting. </summary>
        event EventHandler<DeviceChangedEventArgs> Connecting;

        /// <summary> Occurs when a device is disconnecting. </summary>
        event EventHandler<DeviceChangedEventArgs> Disconnecting;

        /// <summary>
        /// Get connected device count.
        /// </summary>
        int GetDevicesCount();

        /// <summary>
        /// Gets the device at index.
        /// </summary>
        TDevice GetDevice(int index);

        /// <summary>
        /// Get the current state of the device at index.
        /// </summary>
        TState GetCurrentState(int index);
    }

    /// <summary>
    /// Interface that represents a mouse.
    /// </summary>
    public interface IMouse : IDevice<MouseState>
    {
        /// <summary> Occurs when the mouse pointer moves. </summary>
        event EventHandler<MouseEventArgs> Move;

        /// <summary> Occurs when a mouse wheel moved. </summary>
        event EventHandler<MouseWheelEventArgs> MouseWheel;

        /// <summary> Occurs when a mouse button is pressed. </summary>
        event EventHandler<MouseButtonEventArgs> MouseDown;

        /// <summary> Occurs when a mouse button is released. </summary>
        event EventHandler<MouseButtonEventArgs> MouseUp;

        /// <summary>
        /// Sets the mouse cursor position.
        /// </summary>
        void SetPosition(int x, int y);

        /// <summary>
        /// Gets the mouse cursor position.
        /// </summary>
        void GetPosition(out int x, out int y);

        // TODO: Mouse Cursor Image
    }

    /// <summary>
    /// Interface that represents a keyboard.
    /// </summary>
    public interface IKeyboard : IDevice<KeyboardState>
    {
        /// <summary> Occurs when a key is pressed. </summary>
        event EventHandler<KeyEventArgs> KeyDown;

        /// <summary> Occurs when a key is released. </summary>
        event EventHandler<KeyEventArgs> KeyUp;

        /// <summary>
        /// Gets the <see cref="TextInput"/>.
        /// </summary>
        TextInput TextInput { get; }
    }

    /// <summary>
    /// Interface that represents a touch device.
    /// </summary>
    public interface ITouchDevice : IDevice<TouchCollection>
    {

    }

    /// <summary>
    /// Interface that represent gamepads.
    /// </summary>
    public interface IGamePads<TDevice> : IDevices<TDevice, GamePadState>
        where TDevice : IDevice<GamePadState>
    {
        /// <summary> Occurs when a button is pressed. </summary>
        event EventHandler<GamePadEventArgs> buttonDown;

        /// <summary> Occurs when a button is released. </summary>
        event EventHandler<GamePadEventArgs> buttonUp;
    }
}
