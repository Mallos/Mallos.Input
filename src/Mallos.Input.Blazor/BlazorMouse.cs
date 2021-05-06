namespace Mallos.Input.Blazor
{
    using Mallos.Input.Trackers;
    using System.Threading.Tasks;

    public class BlazorMouse : BlazorDevice, IMouse
    {
        /// <inheritdoc />
        public string Name => "Blazor Mouse";

        /// <inheritdoc />
        public IMouseTracker CreateTracker() => new BasicMouseTracker(this);

        /// <inheritdoc />
        public MouseState GetCurrentState() => BlazorMouseState.GetSnapshot();

        /// <inheritdoc />
        public void GetPosition(out int x, out int y)
        {
            var currentState = this.GetCurrentState();
            x = currentState.X;
            y = currentState.Y;
        }

        /// <inheritdoc />
        public void SetPosition(int x, int y)
        {
            throw new System.NotImplementedException();
        }
    }

    public static class BlazorMouseState
    {
        private static int x;
        private static int y;
        private static int scrollWheel;
        private static bool leftButton;
        private static bool middleButton;
        private static bool rightButton;
        private static bool xButton1;
        private static bool xButton2;

        public static MouseState GetSnapshot()
        {
            return new MouseState(x, y,
                scrollWheel,
                leftButton, middleButton, rightButton,
                xButton1, xButton2
            );
        }

        public static ValueTask OnMouseMove(int mouseX, int mouseY)
        {
            x = mouseX;
            y = mouseY;
            return ValueTask.CompletedTask;
        }

        public static ValueTask OnMouseWheel(long delta)
        {
            scrollWheel += (int)delta;
            return ValueTask.CompletedTask;
        }

        public static ValueTask OnMouseToggle(int button, bool down)
        {
            switch ((BlazorMouseButtons)button)
            {
                case BlazorMouseButtons.LeftButton:
                    leftButton = down;
                    break;

                case BlazorMouseButtons.MiddleButton:
                    middleButton = down;
                    break;

                case BlazorMouseButtons.RightButton:
                    rightButton = down;
                    break;

                case BlazorMouseButtons.XButton1:
                    xButton1 = down;
                    break;

                case BlazorMouseButtons.XButton2:
                    xButton2 = down;
                    break;
            }

            return ValueTask.CompletedTask;
        }
    }
}
