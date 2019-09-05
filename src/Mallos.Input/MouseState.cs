namespace Mallos.Input
{
    using System.Numerics;

    /// <summary>
    /// Represents specific state of a mouse.
    /// </summary>
    public struct MouseState
    {
        public static readonly MouseState Empty = new MouseState(0, 0, 0, MouseButtons.Empty);

        /// <summary>
        /// Initializes a new instance of the <see cref="MouseState"/> struct.
        /// </summary>
        public MouseState(int x, int y, int scrollWheel, MouseButtons buttons)
        {
            this.X = x;
            this.Y = y;
            this.ScrollWheelValue = scrollWheel;
            this.PressedButtons = buttons;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MouseState"/> struct.
        /// </summary>
        [System.Obsolete("This will be removed in future versions.")]
        public MouseState(int x, int y,
            int scrollWheel,
            bool leftButton, bool middleButton, bool rightButton,
            bool xButton1, bool xButton2)
        {
            this.X = x;
            this.Y = y;
            this.ScrollWheelValue = scrollWheel;

            this.PressedButtons = MouseButtons.Empty;

            if (leftButton) this.PressedButtons |= MouseButtons.Left;
            if (middleButton) this.PressedButtons |= MouseButtons.Middle;
            if (rightButton) this.PressedButtons |= MouseButtons.Right;
            if (xButton1) this.PressedButtons |= MouseButtons.XButton1;
            if (xButton2) this.PressedButtons |= MouseButtons.XButton2;
        }

        /// <summary>
        /// Gets the mouse x coords of this state.
        /// </summary>
        public int X { get; }

        /// <summary>
        /// Gets the mouse y coords of this state.
        /// </summary>
        public int Y { get; }

        /// <summary>
        /// Gets a <see cref="Vector2"/> of the cursor position.
        /// </summary>
        public Vector2 Position => new Vector2(X, Y);

        /// <summary>
        /// Gets or sets the scroll wheel value of this state.
        /// </summary>
        public int ScrollWheelValue { get; }

        /// <summary>
        /// Gets all the pressed buttons.
        /// </summary>
        public MouseButtons PressedButtons { get; }

        /// <summary>
        /// Gets wether the left button is pressed.
        /// </summary>
        public bool LeftButton => this.PressedButtons.HasFlag(MouseButtons.Left);

        /// <summary>
        /// Gets wether the middle button is pressed.
        /// </summary>
        public bool MiddleButton => this.PressedButtons.HasFlag(MouseButtons.Middle);

        /// <summary>
        /// Gets wether the right button is pressed.
        /// </summary>
        public bool RightButton => this.PressedButtons.HasFlag(MouseButtons.Right);

        /// <summary>
        /// Gets wether the extra button 1 is pressed.
        /// </summary>
        public bool XButton1 => this.PressedButtons.HasFlag(MouseButtons.XButton1);

        /// <summary>
        /// Gets wether the extra button 2 is pressed.
        /// </summary>
        public bool XButton2 => this.PressedButtons.HasFlag(MouseButtons.XButton2);

        /// <summary>
        /// Returns wether or not the button is pressed.
        /// </summary>
        public bool IsButtonDown(MouseButtons button)
        {
            return this.PressedButtons.HasFlag(button);
        }

        public override string ToString()
        {
            return $"{X}, {Y}, {ScrollWheelValue}, {LeftButton}, {MiddleButton}, {RightButton}, {XButton1}, {XButton2}";
        }
    }
}
