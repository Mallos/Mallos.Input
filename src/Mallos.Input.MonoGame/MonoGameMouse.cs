namespace Mallos.Input.MonoGame
{
    using Mallos.Input.Trackers;

    public class MonoGameMouse : MonoGameDevice, IMouse
    {
        public string Name => "Mouse";

        private int lastScrollWheelValue = 0;

        public IMouseTracker CreateTracker()
            => new BasicMouseTracker(this);

        public MouseState GetCurrentState()
        {
            Microsoft.Xna.Framework.Input.MouseState state = Microsoft.Xna.Framework.Input.Mouse.GetState();

            MouseState newState = new(
                x: state.X,
                y: state.Y,
                scrollWheel: state.ScrollWheelValue - this.lastScrollWheelValue,
                leftButton: state.LeftButton == Microsoft.Xna.Framework.Input.ButtonState.Pressed,
                middleButton: state.MiddleButton == Microsoft.Xna.Framework.Input.ButtonState.Pressed,
                rightButton: state.RightButton == Microsoft.Xna.Framework.Input.ButtonState.Pressed,
                xButton1: state.XButton1 == Microsoft.Xna.Framework.Input.ButtonState.Pressed,
                xButton2: state.XButton2 == Microsoft.Xna.Framework.Input.ButtonState.Pressed
            );

            this.lastScrollWheelValue = state.ScrollWheelValue;
            return newState;
        }

        public void GetPosition(out int x, out int y)
        {
            Microsoft.Xna.Framework.Input.MouseState state = Microsoft.Xna.Framework.Input.Mouse.GetState();

            x = state.X;
            y = state.Y;
        }

        public void SetPosition(int x, int y)
        {
            throw new System.NotSupportedException();
        }
    }
}
