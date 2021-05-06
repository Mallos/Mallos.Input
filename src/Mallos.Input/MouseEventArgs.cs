namespace Mallos.Input
{
    using System;

    /// <summary>
    /// Mouse event args.
    /// </summary>
    public class MouseEventArgs : EventArgs
    {
        /// <summary>
        /// Gets the state as of when the event occurs.
        /// </summary>
        public MouseState State { get; internal set; }

        /// <summary>
        /// Initializes a new instance of <see cref="MouseEventArgs"/>.
        /// </summary>
        public MouseEventArgs(MouseState state)
        {
            this.State = state;
        }

        public override string ToString()
            => $"<MouseEventArgs, State: {this.State}>";
    }

    /// <summary>
    /// Mouse event args, for when a button is pressed.
    /// </summary>
    public class MouseButtonEventArgs : MouseEventArgs
    {

        /// <summary>
        /// Gets the button that was pressed.
        /// </summar>
        public MouseButtons Button { get; }

        /// <summary>
        /// Initializes a new instance of <see cref="MouseButtonEventArgs"/>.
        /// </summary>
        public MouseButtonEventArgs(MouseState state, MouseButtons button)
            : base(state)
        {
            this.Button = button;
        }

        public override string ToString()
            => $"<MouseEventArgs, State: {this.State}, Button: {this.Button}>";
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

        public override string ToString()
            => $"<MouseWheelEventArgs, State: {this.State}>";
    }
}