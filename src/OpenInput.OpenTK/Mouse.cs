using OpenTK.Input;
using tkMouse = OpenTK.Input.Mouse;

namespace OpenInput.OpenTK
{
    /// <summary>
    /// 
    /// </summary>
    public class Mouse : IMouse
    {
        /// <inheritdoc />
        public string Name => "";

        private static int wheelPrevius = 0;
        private static MouseState mouseState = new MouseState();

        /// <summary>
        /// Initializes a new instance of the <see cref="Mouse"/> class.
        /// </summary>
        public Mouse()
        {

        }

        /// <inheritdoc />
        public MouseState GetCurrentState()
        {
            var state = tkMouse.GetState();

            mouseState.X = state.X;
            mouseState.Y = state.Y;
            mouseState.ScrollWheelDelta = wheelPrevius - state.ScrollWheelValue;
            mouseState.ScrollWheelValue = state.ScrollWheelValue;
            mouseState.LeftButton = state.LeftButton == ButtonState.Pressed;
            mouseState.MiddleButton = state.MiddleButton == ButtonState.Pressed;
            mouseState.RightButton = state.RightButton == ButtonState.Pressed;
            mouseState.XButton1 = state.XButton1 == ButtonState.Pressed;
            mouseState.XButton2 = state.XButton2 == ButtonState.Pressed;

            return mouseState;
        }

        /// <inheritdoc />
        public void SetPosition(int x, int y)
        {
            tkMouse.SetPosition(x, y);
        }
    }
}
