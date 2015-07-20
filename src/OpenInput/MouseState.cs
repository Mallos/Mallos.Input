namespace OpenInput
{
    using System.Numerics;

    /// <summary>
    /// Represents specific state of a mouse.
    /// </summary>
    public struct MouseState
    {
        /// <summary> Gets or sets the mouse x coords of this state. </summary>
        public int X { get; set; }

        /// <summary> Gets or sets the mouse y coords of this state. </summary>
        public int Y { get; set; }

        /// <summary> Gets a <see cref="Vector2"/> of the cursor position. </summary>
        public Vector2 Position => new Vector2(X, Y);

        /// <summary> Gets or sets the scroll wheel value of this state. </summary>
        public int ScrollWheelValue { get; set; }
        
        public bool LeftButton { get; set; }
        public bool MiddleButton { get; set; }
        public bool RightButton { get; set; }

        public bool XButton1 { get; set; }
        public bool XButton2 { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="MouseState"/> struct.
        /// </summary>
        public MouseState(int x, int y, 
            int scrollWheel, 
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

        public override string ToString()
        {
            return $"{X}, {Y}, {ScrollWheelValue}, {LeftButton}, {MiddleButton}, {RightButton}, {XButton1}, {XButton2}";
        }
    }
}
