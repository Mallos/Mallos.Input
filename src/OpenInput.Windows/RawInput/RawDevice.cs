namespace OpenInput.RawInput
{
    using System;

    struct RawDeviceInfo
    {
        /// <summary> i.e. \\?\HID#VID_045E&PID_00DD&MI_00#8&1eb402&0&0000#{884b96c3-56ef-11d1-bc8c-00a0c91405dd} </summary>
        public string DeviceName;

        /// <summary> KEYBOARD or HID </summary>
        public string DeviceType;

        /// <summary> Handle to the device that send the input </summary>
        public IntPtr DeviceHandle;

        /// <summary> i.e. Microsoft USB Comfort Curve Keyboard 2000 (Mouse and Keyboard Center) </summary>
        public string DeviceDescName;
    }

    public abstract class RawDevice
    {
        // TODO: This limits the program to only use one handle.
        //       We do capture global input, so this is is not required.
        internal static DeviceService Service { get; private set; }

        protected RawDevice(IntPtr handle)
        {
            if (Service == null)
                Service = new DeviceService(handle);
        }
    }
}
