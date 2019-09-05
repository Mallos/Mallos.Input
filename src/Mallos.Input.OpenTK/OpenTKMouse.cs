namespace Mallos.Input
{
    using OpenTK.Input;
    using System;
    using tkMouse = OpenTK.Input.Mouse;
    using Mallos.Input.Trackers;
    using OpenTK;

    /// <summary>
    /// OpenTK Mouse.
    /// </summary>
    public class OpenTKMouse : OpenTKDevice<MouseDevice>, IMouse
    {
        /// <inheritdoc />
        public string Name => HasDevice ? Device.Description : "OpenTK Mouse";
        
        public NativeWindow Window { get; }

        private static MouseState mouseState = new MouseState();

        /// <summary>
        /// Initializes a new instance of the <see cref="OpenTKMouse"/> class.
        /// </summary>
        /// <param name="mouseDevice"></param>
        public OpenTKMouse(NativeWindow window, MouseDevice mouseDevice = null)
            : base(mouseDevice)
        {
            this.Window = window ?? throw new ArgumentNullException(nameof(window));
        }

        /// <inheritdoc />
        public IMouseTracker CreateTracker()
        {
            return new BasicMouseTracker(this);
        }

        /// <inheritdoc />
        public void SetPosition(int x, int y)
        {
            tkMouse.SetPosition(x, y);
        }

        /// <inheritdoc />
        public void GetPosition(out int x, out int y)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public MouseState GetCurrentState()
        {
            var state = HasDevice ? Device.GetCursorState() : tkMouse.GetCursorState();

            // TODO: Check if we have window instead?
            mouseState.X = state.X - Window.Location.X;
            mouseState.Y = state.Y - Window.Location.Y;
            mouseState.ScrollWheelValue = state.ScrollWheelValue;
            mouseState.LeftButton = state.LeftButton == ButtonState.Pressed;
            mouseState.MiddleButton = state.MiddleButton == ButtonState.Pressed;
            mouseState.RightButton = state.RightButton == ButtonState.Pressed;
            mouseState.XButton1 = state.XButton1 == ButtonState.Pressed;
            mouseState.XButton2 = state.XButton2 == ButtonState.Pressed;

            return mouseState;
        }
    }
}
