namespace OpenInput
{
    using OpenTK.Input;
    using System;
    using tkMouse = OpenTK.Input.Mouse;
    using OpenInput.Trackers;

    /// <summary>
    /// OpenTK Mouse.
    /// </summary>
    public class OpenTKMouse : OpenTKDevice<MouseDevice>, IMouse
    {
        /// <inheritdoc />
        public string Name => HasDevice ? Device.Description : "OpenTK Mouse";
        
        private static MouseState mouseState = new MouseState();

        /// <summary>
        /// Initializes a new instance of the <see cref="OpenTKMouse"/> class.
        /// </summary>
        /// <param name="mouseDevice"></param>
        public OpenTKMouse(MouseDevice mouseDevice = null)
            : base(mouseDevice)
        {

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
            var state = HasDevice ? Device.GetState() : tkMouse.GetState();

            mouseState.X = state.X;
            mouseState.Y = state.Y;
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
