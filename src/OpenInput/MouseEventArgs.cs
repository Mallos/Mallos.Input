namespace OpenInput
{
    using System;

    /// <summary>
    /// Mouse event args.
    /// </summary>
    public class MouseEventArgs : EventArgs
    {
        /// <summary>
        /// Initializes a new instance of <see cref="MouseEventArgs"/>.
        /// </summary>
        public MouseEventArgs(MouseState state)
        {
            this.State = state;
        }

        /// <summary>
        /// Gets the state as of when the event occurs.
        /// </summary>
        public MouseState State { get; internal set; }
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
}