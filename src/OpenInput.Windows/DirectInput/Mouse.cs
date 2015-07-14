namespace OpenInput.DirectInput
{
    using DirectInputMouse = SharpDX.DirectInput.Mouse;

    /// <summary>
    /// DirectInput Mouse
    /// </summary>
    /// <remarks>
    /// * You basically need to set the cursor position to (0,0) so you can get right coords.
    /// "There's one occasion where it's acceptable to use DirectInput for mouse input, 
    /// and that's if you need high DPI mouse input and you need to support pre-Win2k machine."
    /// http://www.gamedev.net/blog/233/entry-1567278-reasons-not-to-use-directinput-for-keyboard-input/
    /// </remarks>
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

            this.SetPosition(0, 0);
        }
        
        //public void SetHandle(IntPtr handle)
        //{
        //    // ApiCode: [DIERR_INPUTLOST/InputLost], Message: The system cannot read from the specified device.
        //    //if (!mouse.IsDisposed)
        //    //{
        //    //    mouse.Unacquire();
        //    //    mouse.SetCooperativeLevel(handle, CooperativeLevel.Foreground | CooperativeLevel.NonExclusive);
        //    //    mouse.Acquire();
        //    //}
        //}

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
