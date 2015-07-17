namespace OpenInput.DirectInput
{
    using DirectInputMouse = SharpDX.DirectInput.Mouse;

    /// <summary>
    /// DirectInput Mouse
    /// </summary>
    public class Mouse : IMouse
    {
        /// <inheritdoc />
        public string Name => mouse.Information.ProductName.Trim('\0');

        internal readonly DirectInputMouse mouse;

        private MouseState state;

        /// <summary>
        /// Initializes a new instance of the <see cref="Mouse"/> class.
        /// </summary>
        public Mouse()
        {
            var directInput = DeviceService.Service.Value.directInput;

            this.state = new MouseState();
            this.mouse = new DirectInputMouse(directInput);
            this.mouse.Acquire();

            // TODO: Create a win32 method that sets the cursor to the middle of the screen or gets the coords
            this.SetPosition(0, 0);
        }
        
        /// <inheritdoc />
        public void SetPosition(int x, int y)
        {
            System.Windows.Forms.Cursor.Position = new System.Drawing.Point(x, y);
        }

        /// <inheritdoc />
        public MouseState GetCurrentState()
        {
            if (mouse.IsDisposed)
                return new MouseState();

            mouse.Poll();

            var state = mouse.GetCurrentState();

            this.state.X += state.X;
            this.state.Y += state.Y;

            var screenBounds = DeviceService.Service.Value.ScreenBounds;
            if (this.state.X < screenBounds.X) this.state.X = screenBounds.X;
            if (this.state.Y < screenBounds.Y) this.state.Y = screenBounds.Y;
            if (this.state.X > screenBounds.Width) this.state.X = screenBounds.Width;
            if (this.state.Y > screenBounds.Height) this.state.Y = screenBounds.Height;

            this.state.ScrollWheelDelta = state.Z;
            this.state.ScrollWheelValue += state.Z;
            this.state.LeftButton = state.Buttons[0];
            this.state.MiddleButton = state.Buttons[2];
            this.state.RightButton = state.Buttons[1];
            this.state.XButton1 = state.Buttons[3];
            this.state.XButton2 = state.Buttons[4];
            
            return this.state;
        }
    }
}
