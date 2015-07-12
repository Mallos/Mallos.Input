namespace OpenInput
{
    using System;
    using DI_Mouse = SharpDX.DirectInput.Mouse;

    /// <summary>
    /// 
    /// </summary>
    public class Mouse : IMouse
    {
        /// <inheritdoc />
        public string Name => mouse.Information.ProductName.Trim('\0');

        internal readonly DI_Mouse mouse;

        /// <summary>
        /// 
        /// </summary>
        public Mouse()
        {
            var directInput = DeviceService.Service.Value.directInput;

            this.mouse = new DI_Mouse(directInput);
            this.mouse.Acquire();
        }

        /// <inheritdoc />
        public void SetHandle(IntPtr handle)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public void SetPosition(int x, int y)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public MouseState GetCurrentState()
        {
            throw new NotImplementedException();
        }
    }
}
