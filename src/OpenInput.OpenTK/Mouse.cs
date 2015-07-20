using OpenTK.Input;
using System;
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

        /// <inheritdoc />
        public event EventHandler<MouseButtonEventArgs> MouseDown;

        /// <inheritdoc />
        public event EventHandler<MouseButtonEventArgs> MouseUp;

        /// <inheritdoc />
        public event EventHandler<MouseWheelEventArgs> MouseWheel;

        /// <inheritdoc />
        public event EventHandler<MouseEventArgs> Move;

        private static MouseState mouseState = new MouseState();

        /// <summary>
        /// Initializes a new instance of the <see cref="Mouse"/> class.
        /// </summary>
        public Mouse()
        {

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
            var state = tkMouse.GetState();

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
