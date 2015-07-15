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
        /// Gets the <see cref="ITextInput"/>.
        /// </summary>
        ITextInput TextInput { get; }
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
    public interface IGamePad: IDevice<GamePadState>
    {
        /// <summary>
        /// Gets the current state --
        /// </summary>
        GamePadState GetCurrentState(int index);
    }

    /// <summary>
    /// Interface that provides support for text input.
    /// </summary>
    public interface ITextInput
    {
        /// <summary>
        /// Gets or sets the result.
        /// </summary>
        string Result { get; set; }

        /// <summary>
        /// Gets or sets, if capturing input.
        /// </summary>
        bool Capture { get; set; }
        
        /// <summary>
        /// Gets or sets, if the capture should allow new lines.
        /// </summary>
        bool AllowNewLine { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public interface IDeviceService
    {
        int NumberOfKeyboards { get; }
    }
}
