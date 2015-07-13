namespace OpenInput.RawInput
{
    using System;
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

        readonly object _padLock = new object();
        static InputData rawBuffer;

        event DeviceEventHandler KeyPressed;

        /// <summary>
        /// Initializes a new instance of the <see cref="Keyboard"/> class.
        /// </summary>
        public Keyboard(bool captureInBackground = false)
            : this(IntPtr.Zero, captureInBackground)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Keyboard"/> class.
        /// </summary>
        public Keyboard(IntPtr hwnd, bool captureInBackground = false)
        {
            var rid = new RawInputDevice[1];

            rid[0].UsagePage = HidUsagePage.GENERIC;
            rid[0].Usage = HidUsage.Keyboard;
            rid[0].Flags = (captureInBackground ? RawInputDeviceFlags.NONE : RawInputDeviceFlags.INPUTSINK) | RawInputDeviceFlags.DEVNOTIFY;

            if (hwnd == IntPtr.Zero)
            {
                rid[0].Target = hwnd;
            }

            if (!Win32.RegisterRawInputDevices(rid, (uint)rid.Length, (uint)Marshal.SizeOf(rid[0])))
            {
                throw new ApplicationException("Failed to register raw input device(s).");
            }
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
