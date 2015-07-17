namespace OpenInput.RawInput
{
    using System;
    using System.Linq;
    using System.Runtime.InteropServices;

    // TODO: Implement ITextInput

    /// <summary>
    /// 
    /// </summary>
    public class Keyboard : RawDevice, IKeyboard
    {
        /// <inheritdoc />
        public string Name => Service.KeyboardNames;

        /// <inheritdoc />
        public TextInput TextInput => textInput;
        private TextInput textInput = new Empty.TextInput();

        /// <summary>
        /// Initializes a new instance of the <see cref="Keyboard"/> class.
        /// </summary>
        public Keyboard(IntPtr handle, bool captureInBackground = false)
            : base(handle)
        {
            var rid = new RawInputDevice[1];
            rid[0].UsagePage = HidUsagePage.GENERIC;
            rid[0].Usage = HidUsage.Keyboard;
            rid[0].Flags = RawInputDeviceFlags.INPUTSINK | RawInputDeviceFlags.DEVNOTIFY;
            rid[0].Target = handle;

            if (!Win32.RegisterRawInputDevices(rid, (uint)rid.Length, (uint)Marshal.SizeOf(rid[0])))
                throw new ApplicationException("Failed to register raw input device(s).");
        }

        /// <inheritdoc />
        public KeyboardState GetCurrentState()
        {
            return new KeyboardState(Service.Keys.ToArray());
        }
    }
}
