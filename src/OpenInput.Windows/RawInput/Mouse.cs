namespace OpenInput.RawInput
{
    using System;

    public class Mouse : IMouse
    {
        public string Name
        {
            get
            {
                throw new NotImplementedException();
            }
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
