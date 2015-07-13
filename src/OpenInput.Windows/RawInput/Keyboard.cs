namespace OpenInput.RawInput
{
    using System;
    using System.Diagnostics;
    using System.Runtime.InteropServices;

    delegate void DeviceEventHandler(object sender, RawInputEventArg e);

    public class Keyboard : IKeyboard
    {
        public string Name
        {
            get
            {
                throw new NotImplementedException();
            }
        }

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
            throw new NotImplementedException();
        }

        public void SetHandle(IntPtr handle)
        {
            throw new NotImplementedException();
        }
    }
}
