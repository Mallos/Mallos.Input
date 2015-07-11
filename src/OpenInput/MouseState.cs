namespace OpenInput
{
    public struct MouseState
    {
        public int X { get; internal set; }
        public int Y { get; internal set; }

        public int ScrollWheelValue { get; internal set; }

        public bool LeftButton { get; internal set; }
        public bool MiddleButton { get; internal set; }
        public bool RightButton { get; internal set; }

        public bool XButton1 { get; internal set; }
        public bool XButton2 { get; internal set; }
        
        public MouseState(int x, int y, int scrollWheel,
            bool leftButton, bool middleButton, bool rightButton,
            bool xButton1, bool xButton2)
        {
            this.X = x;
            this.Y = y;
            this.ScrollWheelValue = scrollWheel;

            this.LeftButton = leftButton;
            this.MiddleButton = middleButton;
            this.RightButton = rightButton;

            this.XButton1 = xButton1;
            this.XButton2 = xButton2;
        }
    }
}
