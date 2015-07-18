namespace OpenInput.RawInput
{
    using System;
    using System.Runtime.InteropServices;

    /// <summary>
    /// Class that represents a mouse, for RawInput.
    /// </summary>
    public class Mouse : RawDevice, IMouse
    {
        /// <inheritdoc />
        public string Name => Service.MouseNames;

        /// <inheritdoc />
        public event EventHandler<MouseEventArgs> Move;

        /// <inheritdoc />
        public event EventHandler<MouseWheelEventArgs> MouseWheel;

        /// <inheritdoc />
        public event EventHandler<MouseButtonEventArgs> MouseDown;

        /// <inheritdoc />
        public event EventHandler<MouseButtonEventArgs> MouseUp;

        /// <summary>
        /// Initializes a new instance of the <see cref="Mouse"/> class.
        /// </summary>
        public Mouse(IntPtr handle)
            : base(handle)
        {
            // TODO: Move device register location
            //       I don't really like registering it here since there should only be one registered,
            //       What if the user creates two instances?

            var rid = new RawInputDevice[1];
            rid[0].UsagePage = HidUsagePage.GENERIC;
            rid[0].Usage = HidUsage.Mouse;
            rid[0].Flags = RawInputDeviceFlags.INPUTSINK | RawInputDeviceFlags.DEVNOTIFY;
            rid[0].Target = handle;

            if (!WindowsInterop.RegisterRawInputDevices(rid, (uint)rid.Length, (uint)Marshal.SizeOf(rid[0])))
                throw new ApplicationException("Failed to register raw input device(s).");
        }

        /// <inheritdoc />
        public void SetPosition(int x, int y)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public void GetPosition(out int x, out int y)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public MouseState GetCurrentState()
        {
            return Service.MouseState;
        }
    }
}
