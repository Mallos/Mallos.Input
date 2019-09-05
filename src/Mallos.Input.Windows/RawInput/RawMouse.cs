namespace Mallos.Input
{
    using Mallos.Input.RawInput;
    using Mallos.Input.Trackers;
    using System;

    /// <summary>
    /// Class that represents a mouse, for RawInput.
    /// </summary>
    public class RawMouse : RawDevice, IMouse
    {
        /// <inheritdoc />
        public string Name => Service.MouseNames;
        
        /// <summary>
        /// Initializes a new instance of the <see cref="RawMouse"/> class.
        /// </summary>
        public RawMouse(IntPtr windowHandle)
            : base(windowHandle)
        {

        }

        public IMouseTracker CreateTracker()
        {
            return new BasicMouseTracker(this);
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
            if (Service == null)
            {
                return MouseState.Empty;
            }
            return Service.MouseState;
        }
    }
}
