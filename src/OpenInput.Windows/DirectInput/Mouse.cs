namespace OpenInput.DirectInput
{
    using SharpDX;
    using System;
    using CooperativeLevel = SharpDX.DirectInput.CooperativeLevel;
    using DirectInputMouse = SharpDX.DirectInput.Mouse;

    /// <summary>
    /// 
    /// </summary>
    public class Mouse : IMouse
    {
        /// <inheritdoc />
        public string Name => mouse.Information.ProductName.Trim('\0');

        internal readonly DirectInputMouse mouse;

        private MouseState state;

        /// <summary>
        /// 
        /// </summary>
        public Mouse()
        {
            var directInput = DeviceService.Service.Value.directInput;

            this.state = new MouseState();
            this.mouse = new DirectInputMouse(directInput);
            this.mouse.Acquire();
        }

        /// <inheritdoc />
        public void SetHandle(IntPtr handle)
        {
            if (!mouse.IsDisposed)
            {
                mouse.Unacquire();
                mouse.SetCooperativeLevel(handle, CooperativeLevel.Foreground | CooperativeLevel.NonExclusive);
                mouse.Acquire();
            }
        }

        /// <inheritdoc />
        public void SetPosition(int x, int y)
        {
            if (!mouse.IsDisposed)
            {
                mouse.Poll();

                var state = mouse.GetCurrentState();
                //state.Update(new SharpDX.DirectInput.MouseUpdate())

                // TODO: How can I set the mouse position?
            }
        }

        /// <inheritdoc />
        public MouseState GetCurrentState()
        {
            if (mouse.IsDisposed)
                return new MouseState();

            // Same issue as Keyboard
            try
            {
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

            }
            catch (SharpDXException e)
            {

            }

            return this.state;
        }
    }
}
