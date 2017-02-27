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
            throw new NotImplementedException();
        }
    }
}
