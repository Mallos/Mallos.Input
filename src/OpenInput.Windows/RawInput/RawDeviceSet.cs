namespace OpenInput
{
    using System;
    using System.Diagnostics;

    public class RawDeviceSet
        : DeviceSet<RawKeyboard, RawMouse, Dummy.DummyGamePad>
    {
        public RawDeviceSet(IntPtr? windowHandle = null)
            : base("RawInput",
                  new RawKeyboard(windowHandle ?? GetWindowHandle()),
                  new RawMouse(windowHandle ?? GetWindowHandle()),
                  null)
        {
            
        }

        private static IntPtr GetWindowHandle() => Process.GetCurrentProcess().Handle;
    }
}
