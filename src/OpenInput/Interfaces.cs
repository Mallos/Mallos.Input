namespace OpenInput
{
    using System;

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
        /// Sets the mouse handle.
        /// </summary>
        void SetHandle(IntPtr handle);

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
        /// Sets the keyboard handle.
        /// </summary>
        void SetHandle(IntPtr handle);

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
    public interface IGamePad: IDevice<KeyboardState>
    {
        /// <summary>
        /// Gets the current state --
        /// </summary>
        KeyboardState GetCurrentState(int index);
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
}
