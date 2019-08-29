namespace OpenInput
{
    using System;

    #region DeviceEventArgs
    /// <summary>
    /// Device event args.
    /// </summary>
    public class DeviceChangedEventArgs : EventArgs
    {
        /// <summary> Gets the associated device. </summary>
        public IDevice Device { get; internal set; }

        /// <summary>
        /// Initializes a new instance of <see cref="DeviceChangedEventArgs"/>.
        /// </summary>
        public DeviceChangedEventArgs(IDevice device)
        {
            this.Device = device;
        }
    }
    #endregion

    #region MouseEventArgs
    /// <summary>
    /// Mouse event args.
    /// </summary>
    public class MouseEventArgs : EventArgs
    {
        /// <summary> Gets the state as of when the event occurs. </summary>
        public MouseState State { get; internal set; }

        /// <summary>
        /// Initializes a new instance of <see cref="MouseEventArgs"/>.
        /// </summary>
        public MouseEventArgs(MouseState state)
        {
            this.State = state;
        }
    }

    /// <summary>
    /// Mouse event args, for when a button is pressed.
    /// </summary>
    public class MouseButtonEventArgs : MouseEventArgs
    {
        /// <summary>
        /// Initializes a new instance of <see cref="MouseButtonEventArgs"/>.
        /// </summary>
        public MouseButtonEventArgs(MouseState state, MouseButtons button)
            : base(state)
        {
            this.Button = button;
        }

        /// <summary>
        /// Gets the button that was pressed.
        /// </summar>
        public MouseButtons Button { get; }
    }

    /// <summary>
    /// Mouse event args, for when mouse wheel has changed.
    /// </summary>
    public class MouseWheelEventArgs : MouseEventArgs
    {
        /// <summary>
        /// Initializes a new instance of <see cref="MouseWheelEventArgs"/>.
        /// </summary>
        public MouseWheelEventArgs(MouseState state)
            : base(state)
        {

        }
    }
    #endregion

    #region KeyboardEventArgs
    /// <summary>
    /// Keyboard event args, for when a key is pressed.
    /// </summary>
    public class KeyEventArgs : EventArgs
    {
        /// <summary>
        /// Gets the pressed key.
        /// </summary>
        public Keys Key { get; internal set; }

        /// <summary>
        /// Gets the char of the key.
        /// </summary>
        public char KeyChar { get; internal set; }
        // TODO: Change it to string?

        /// <summary>
        /// Gets the state as of when the event occurs.
        /// </summary>
        public KeyboardState State { get; internal set; }

        /// <summary>
        /// Initializes a new instance of <see cref="KeyEventArgs"/>.
        /// </summary>
        public KeyEventArgs(KeyboardState state, Keys key, char keyChar)
        {
            this.Key = key;
            this.State = state;
            this.KeyChar = keyChar;
        }
    }
    #endregion

    #region TouchEventArgs
    #endregion

    #region GamePadEventArgs
    /// <summary>
    /// GamePad event args.
    /// </summary>
    public class GamePadEventArgs : EventArgs
    {
        /// <summary> Gets the state as of when the event occurs. </summary>
        public GamePadState State { get; internal set; }

        /// <summary>
        /// Initializes a new instance of <see cref="GamePadEventArgs"/>.
        /// </summary>
        public GamePadEventArgs(GamePadState state)
        {
            this.State = state;
        }
    }
    #endregion
}