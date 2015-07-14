namespace OpenInput.RawInput
{
    using System;
    using System.Runtime.InteropServices;

    /// <summary>
    /// 
    /// </summary>
    public class Keyboard : IKeyboard
    {
        public string Name => "RawInput Keyboard (WIP)";

        public ITextInput TextInput
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Keyboard"/> class.
        /// </summary>
        public Keyboard(IntPtr handle, bool captureInBackground = false)
        {
            new DeviceService(handle);

            var rid = new RawInputDevice[1];
            rid[0].UsagePage = HidUsagePage.GENERIC;
            rid[0].Usage = HidUsage.Keyboard;
            rid[0].Flags = RawInputDeviceFlags.INPUTSINK | RawInputDeviceFlags.DEVNOTIFY;
            rid[0].Target = handle;
            if (!Win32.RegisterRawInputDevices(rid, (uint)rid.Length, (uint)Marshal.SizeOf(rid[0])))
                throw new ApplicationException("Failed to register raw input device(s).");

            //DeviceService.Service.Value.Devices.Add(this);
        }

        public KeyboardState GetCurrentState()
        {
            return new KeyboardState(new Keys[] { });
        }

        public void SetHandle(IntPtr handle)
        {

        }
    }
}
