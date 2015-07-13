namespace OpenInput.RawInput
{
    using System;

    class KeyPressEvent
    {
        /// <summary> i.e. \\?\HID#VID_045E&PID_00DD&MI_00#8&1eb402&0&0000#{884b96c3-56ef-11d1-bc8c-00a0c91405dd} </summary>
        public string DeviceName;
        /// <summary> KEYBOARD or HID </summary>
        public string DeviceType;
        /// <summary> Handle to the device that send the input </summary>
        public IntPtr DeviceHandle;
        /// <summary> i.e. Microsoft USB Comfort Curve Keyboard 2000 (Mouse and Keyboard Center) </summary>
        public string Name;
        /// <summary> Virtual Key. Corrected for L/R keys(i.e. LSHIFT/RSHIFT) and Zoom </summary>
        public int VKey; 
        /// <summary> Virtual Key Name. Corrected for L/R keys(i.e. LSHIFT/RSHIFT) and Zoom </summary>
        public string VKeyName; 
        /// <summary> WM_KEYDOWN or WM_KEYUP </summary>
        public uint Message;
        /// <summary> MAKE or BREAK </summary>     
        public string KeyPressState;

        /// <summary> Keyboard_XX </summary>
        public string Source
        {
            get { return source; }
            set { source = $"Keyboard_{value.PadLeft(2, '0')}"; }
        }
        private string source;

        public override string ToString()
        {
            return $"Device [ DeviceName: {DeviceName}, DeviceType: {DeviceType}, DeviceHandle: {DeviceHandle.ToInt64().ToString("X")}, Name: {Name} ]";
        }
    }
}
