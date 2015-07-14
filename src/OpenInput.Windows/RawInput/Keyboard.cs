namespace OpenInput.RawInput
{
    using System;
    using System.Runtime.InteropServices;

    /// <summary>
    /// 
    /// </summary>
    public class Keyboard : RawDevice, IKeyboard
    {
        /// <inheritdoc />
        public string Name => this.DeviceDescName;

        /// <inheritdoc />
        public ITextInput TextInput => null;

        /// <summary>
        /// Initializes a new instance of the <see cref="Keyboard"/> class.
        /// </summary>
        public Keyboard(IntPtr handle, bool captureInBackground = false)
            : base(handle)
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

        /// <inheritdoc />
        public KeyboardState GetCurrentState()
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public void SetHandle(IntPtr handle)
        {

        }
    }
}
