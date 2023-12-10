namespace Mallos.Input.Blazor
{
    using System.Threading.Tasks;

    internal class BlazorMouseState
    {
        private int x;
        private int y;
        private int scrollWheel;
        private bool leftButton;
        private bool middleButton;
        private bool rightButton;
        private bool xButton1;
        private bool xButton2;

        public MouseState GetSnapshot()
        {
            return new MouseState(x, y,
                scrollWheel,
                leftButton, middleButton, rightButton,
                xButton1, xButton2
            );
        }

        public ValueTask OnMouseMove(int mouseX, int mouseY)
        {
            x = mouseX;
            y = mouseY;
            return ValueTask.CompletedTask;
        }

        public ValueTask OnMouseWheel(long delta)
        {
            scrollWheel += (int)delta;
            return ValueTask.CompletedTask;
        }

        public ValueTask OnMouseToggle(int button, bool down)
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
