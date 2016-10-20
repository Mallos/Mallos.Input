namespace OpenInput.RawInput
{
    using System;

    /// <summary>
    /// Class that represents a mouse, for RawInput.
    /// </summary>
    public class Mouse : RawDevice, IMouse
    {
        /// <inheritdoc />
        public string Name => Service.MouseNames;

        /// <inheritdoc />
        public event EventHandler<MouseEventArgs> Move;

        /// <inheritdoc />
        public event EventHandler<MouseWheelEventArgs> MouseWheel;

        /// <inheritdoc />
        public event EventHandler<MouseButtonEventArgs> MouseDown;

        /// <inheritdoc />
        public event EventHandler<MouseButtonEventArgs> MouseUp;
        
        /// <summary>
        /// Initializes a new instance of the <see cref="Mouse"/> class.
        /// </summary>
        public Mouse(IntPtr windowHandle)
            : base(windowHandle)
        {

        }

        /// <inheritdoc />
        public void SetPosition(int x, int y)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public void GetPosition(out int x, out int y)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public MouseState GetCurrentState()
        {
            return Service.MouseState;
        }
    }
}
