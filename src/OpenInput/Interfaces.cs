namespace OpenInput
{
    // TODO: I have to make some changes so I can support multiple devices of one type
    //       For example I should support multiple GamePads
    //       with RawInput I can also support multiple mices and keyboards; currently it is all in one.

    // TODO: I am not sure on how I should implement IDeviceService, the idea behind it is to give all
    //       the capabilities of the devices and how many are connected. Maybe even events when a new
    //       device is connected and disconnected!

    // TODO: I also would like to support events for all the devices. 
    //       Making it all thread-safe would also be a nice thing. 

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
    public interface IDevice<TState> : IDevice where TState : struct
    {
        /// <summary>
        /// Gets the current state of the device.
        /// </summary>
        TState GetCurrentState();
    }
    
    /// <summary>
    /// 
    /// </summary>
    public interface IDevices<TState> : IDevice<TState> where TState : struct
    {
        /// <summary>
        /// Get connected device count.
        /// </summary>
        int GetDevicesCount();

        /// <summary>
        /// 
        /// </summary>
        TState GetCurrentState(int index);
    }

    /// <summary>
    /// 
    /// </summary>
    public interface IMouse : IDevice<MouseState>
    {
        /// <summary>
        /// Sets the mouse cursor position.
        /// </summary>
        void SetPosition(int x, int y);
    }

    /// <summary>
    /// 
    /// </summary>
    public interface IKeyboard : IDevice<KeyboardState>
    {
        /// <summary>
        /// Gets the <see cref="TextInput"/>.
        /// </summary>
        TextInput TextInput { get; }
    }

    /// <summary>
    /// 
    /// </summary>
    public interface ITouchDevice : IDevice<TouchCollection>
    {

    }

    /// <summary>
    /// 
    /// </summary>
    public interface IGamePad: IDevices<GamePadState>
    {

    }
}
