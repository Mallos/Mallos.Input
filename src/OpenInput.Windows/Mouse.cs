namespace OpenInput.Windows
{
    using System;
    using DI_Mouse = SharpDX.DirectInput.Mouse;

    public class Mouse : IMouse
    {
        public string Name => mouse.Information.ProductName.Trim('\0');

        internal readonly DI_Mouse mouse;

        internal Mouse(DI_Mouse mouse)
        {
            this.mouse = mouse;
            this.mouse.Acquire();
        }

        public MouseState GetCurrentState()
        {
            throw new NotImplementedException();
        }
    }
}
