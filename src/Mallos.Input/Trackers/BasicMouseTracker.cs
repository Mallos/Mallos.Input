namespace Mallos.Input.Trackers
{
    using System;

    /// <summary>
    /// Basic mouse tracker, takes the old and the new state then compares them.
    /// </summary>
    public class BasicMouseTracker : BasicDeviceTracker<IMouseTracker, MouseState>, IMouseTracker
    {
        /// <summary>
        /// Initialize a new <see cref="BasicMouseTracker"/> class.
        /// </summary>
        /// <param name="mouse"></param>
        public BasicMouseTracker(IMouse mouse)
            : base(mouse)
        {
        }

        /// <summary>
        /// Initialize a new <see cref="BasicMouseTracker"/> class.
        /// </summary>
        /// <param name="mouse"></param>
        public BasicMouseTracker(IDevice<IMouseTracker, MouseState> mouse)
            : base(mouse)
        {
        }

        /// <inheritdoc />
        public event EventHandler<MouseEventArgs> Move;

        /// <inheritdoc />
        public event EventHandler<MouseWheelEventArgs> MouseWheel;

        /// <inheritdoc />
        public event EventHandler<MouseButtonEventArgs> MouseDown;

        /// <inheritdoc />
        public event EventHandler<MouseButtonEventArgs> MouseUp;

        protected override void Track(MouseState newState, MouseState oldState)
        {
            if (newState.Position != oldState.Position)
            {
                Move?.Invoke(this, new MouseEventArgs(newState));
            }

            if (newState.ScrollWheelValue != oldState.ScrollWheelValue)
            {
                MouseWheel?.Invoke(this, new MouseWheelEventArgs(newState));
            }

            foreach (MouseButtons mouseButton in Enum.GetValues(typeof(MouseButtons)))
            {
                bool newButton = newState.IsButtonDown(mouseButton);
                bool oldButton = oldState.IsButtonDown(mouseButton);

                if (newButton != oldButton)
                {
                    if (newButton)
                    {
                        this.MouseDown?.Invoke(this, new MouseButtonEventArgs(newState, mouseButton));
                    }
                    else
                    {
                        this.MouseUp?.Invoke(this, new MouseButtonEventArgs(newState, mouseButton));
                    }
                }
            }
        }
    }
}
