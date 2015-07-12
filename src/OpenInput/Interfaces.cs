namespace OpenInput
{
    using System.Collections.Generic;

    public interface IDevice
    {
        /// <summary>
        /// Gets the name of the device.
        /// </summary>
        string Name { get; }
    }

    /// <summary>
    /// 
    /// </summary>
    public interface IDevice<TState> : IDevice where TState : struct
    {
        /// <summary>
        /// Gets the current state of the device.
        /// </summary>
        TState GetCurrentState();
    }

    public interface IMouse : IDevice<MouseState> { }
    public interface IKeyboard : IDevice<KeyboardState> { }

    /// <summary>
    /// 
    /// </summary>
    public interface IDeviceService
    {
        /// <summary>
        /// Gets the current mouse.
        /// </summary>
        IMouse GetMouse();

        /// <summary>
        /// Gets the current keyboard.
        /// </summary>
        IKeyboard GetKeyboard();

        /// <summary>
        /// Gets all the connected devices.
        /// </summary>
        IList<IDevice> GetDevices();
    }

    /// <summary>
    /// 
    /// </summary>
    public interface IDeviceEventPusher<TDevice> where TDevice : IDevice
    {
        /// <summary>
        /// 
        /// </summary>
        void Push(TDevice device);
    }

    /// <summary>
    /// 
    /// </summary>
    public interface ITextInput
    {
        /// <summary>
        /// Gets or sets the result.
        /// </summary>
        string Result { get; set; }

        /// <summary>
        /// Gets or sets if capturing input.
        /// </summary>
        bool Capture { get; set; }
    }
}
