namespace OpenInput.Trackers
{
    using System;

    /// <summary>
    /// Basic mouse tracker, takes the old and the new state then compares them.
    /// </summary>
    public class BasicMouseTracker : BasicDeviceTracker<IMouseTracker, MouseState>, IMouseTracker
    {
        /// <inheritdoc />
        public event EventHandler<MouseEventArgs> Move;

        /// <inheritdoc />
        public event EventHandler<MouseWheelEventArgs> MouseWheel;

        /// <inheritdoc />
        public event EventHandler<MouseButtonEventArgs> MouseDown;

        /// <inheritdoc />
        public event EventHandler<MouseButtonEventArgs> MouseUp;

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

            // FIXME: Optimize

            IsMouseDown(newState, newState.LeftButton, oldState.LeftButton, MouseButtons.Left);
            IsMouseDown(newState, newState.MiddleButton, oldState.MiddleButton, MouseButtons.Middle);
            IsMouseDown(newState, newState.RightButton, oldState.RightButton, MouseButtons.Right);

            IsMouseDown(newState, newState.XButton1, oldState.XButton1, MouseButtons.XButton1);
            IsMouseDown(newState, newState.XButton2, oldState.XButton2, MouseButtons.XButton2);
        }

        private void IsMouseDown(MouseState state, bool value1, bool value2, MouseButtons button)
        {
            if (value1 != value2)
            {
                if (value1) MouseDown?.Invoke(this, new MouseButtonEventArgs(state, button));
                else        MouseUp?.Invoke(this, new MouseButtonEventArgs(state, button));
            }
        }
    }
}
