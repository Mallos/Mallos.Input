namespace OpenInput.RawInput
{
    using System;

    public class Mouse : RawDevice, IMouse
    {
        public string Name
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public Mouse(IntPtr handle)
            : base(handle)
        {

        }

        public MouseState GetCurrentState()
        {
            throw new NotImplementedException();
        }

        public void SetHandle(IntPtr handle)
        {
            throw new NotImplementedException();
        }

        public void SetPosition(int x, int y)
        {
            throw new NotImplementedException();
        }
    }
}
