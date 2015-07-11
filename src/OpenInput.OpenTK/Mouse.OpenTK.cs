namespace OpenInput
{
    using System;

    partial class Mouse : Device
    {
        private void PlatformSetHandle(IntPtr handle)
        {

        }

        private MouseState PlatformGetCurrentState()
        {
            return new MouseState();
        }
    }
}
