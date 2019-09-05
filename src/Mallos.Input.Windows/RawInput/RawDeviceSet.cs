namespace Mallos.Input
{
    using System;
    using System.Diagnostics;

    public class RawDeviceSet : DeviceSet
    {
        public RawDeviceSet(IntPtr? windowHandle = null)
            : base("RawInput",
                  new RawKeyboard(windowHandle ?? GetWindowHandle()),
                  new RawMouse(windowHandle ?? GetWindowHandle()),
                  (int index) => { return new Mallos.Input.Dummy.DummyGamePad(index); })
        {
            
        }

        private static IntPtr GetWindowHandle()
        {
            return Process.GetCurrentProcess().Handle;
        }
    }
}
