namespace OpenInput
{
    /// <summary>
    /// Represents specific state of a mouse.
    /// </summary>
    public struct MouseState
    {
        /// <summary> Gets the mouse x coords. </summary>
        public int X { get; internal set; }

        /// <summary> Gets the mouse y coords. </summary>
        public int Y { get; internal set; }

        /// <summary> Gets the scroll wheel value. </summary>
        public int ScrollWheelValue { get; internal set; }

        /// <summary> Gets the scroll wheel delta. </summary>
        public int ScrollWheelDelta { get; internal set; }

        public bool LeftButton { get; internal set; }
        public bool MiddleButton { get; internal set; }
        public bool RightButton { get; internal set; }

        public bool XButton1 { get; internal set; }
        public bool XButton2 { get; internal set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="MouseState"/> struct.
        /// </summary>
        public MouseState(int x, int y, 
            int scrollWheel, int scrollWheelDelta,
            bool leftButton, bool middleButton, bool rightButton,
            bool xButton1, bool xButton2)
        {
            this.X = x;
            this.Y = y;
            this.ScrollWheelValue = scrollWheel;
            this.ScrollWheelDelta = scrollWheelDelta;

            this.LeftButton = leftButton;
            this.MiddleButton = middleButton;
            this.RightButton = rightButton;

            this.XButton1 = xButton1;
            this.XButton2 = xButton2;
        }

        public override string ToString()
        {
            return $"{X}, {Y}, {ScrollWheelValue}, {LeftButton}, {MiddleButton}, {RightButton}, {XButton1}, {XButton2}";
        }
    }
}
