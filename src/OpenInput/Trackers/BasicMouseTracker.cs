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

            IsMouseDown(newState, newState.LeftButton, oldState.LeftButton);
            IsMouseDown(newState, newState.MiddleButton, oldState.MiddleButton);
            IsMouseDown(newState, newState.RightButton, oldState.RightButton);

            IsMouseDown(newState, newState.XButton1, oldState.XButton1);
            IsMouseDown(newState, newState.XButton2, oldState.XButton2);
        }

        private void IsMouseDown(MouseState state, bool value1, bool value2)
        {
            if (value1 != value2)
            {
                if (value1) MouseDown?.Invoke(this, new MouseButtonEventArgs(state));
                else        MouseUp?.Invoke(this, new MouseButtonEventArgs(state));
            }
        }
    }
}
