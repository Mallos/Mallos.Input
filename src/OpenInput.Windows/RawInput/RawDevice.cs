namespace OpenInput.RawInput
{
    using System;

    public abstract class RawDevice
    {
        // TODO: This limits the program to only use one handle
        internal static DeviceService Service;

        /// <summary> i.e. \\?\HID#VID_045E&PID_00DD&MI_00#8&1eb402&0&0000#{884b96c3-56ef-11d1-bc8c-00a0c91405dd} </summary>
        internal string DeviceName = string.Empty;
        
        /// <summary> KEYBOARD or HID </summary>
        internal string DeviceType;
        
        /// <summary> Handle to the device that send the input </summary>
        internal IntPtr DeviceHandle;

        /// <summary> i.e. Microsoft USB Comfort Curve Keyboard 2000 (Mouse and Keyboard Center) </summary>
        internal string DeviceDescName;

        protected RawDevice(IntPtr handle)
        {
            if (Service == null)
                Service = new DeviceService(handle);
        }
    }
}
