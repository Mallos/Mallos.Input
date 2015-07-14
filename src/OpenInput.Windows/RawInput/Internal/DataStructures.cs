namespace OpenInput.RawInput
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Explicit)]
    struct DeviceInfo
    {
        [FieldOffset(0)] public int Size;
        [FieldOffset(4)] public int Type;
        [FieldOffset(8)] public DeviceMouse Mouse;
        [FieldOffset(8)] public DeviceKeyboard Keyboard;
        [FieldOffset(8)] public DeviceHID HID;

        public override string ToString()
        {
            return $"DeviceInfo [ Size: {Size}, Type: {Type} ]";
        }
    }

    struct DeviceMouse
    {
        /// <summary> Identifier of the mouse device. </summary>
        public uint Id;
        /// <summary> Number of buttons for the mouse. </summary>
        public uint NumberOfButtons;
        /// <summary> Number of data points per second.. </summary>
        public uint SampleRate;
        /// <summary> True is mouse has wheel for horizontal scrolling else false. </summary>
        public bool HasHorizontalWheel;
        
        public override string ToString()
        {
            return $"MouseInfo [ Id: {Id}, NumberOfButtons: {NumberOfButtons}, SampleRate: {SampleRate}, HorizontalWheel: {HasHorizontalWheel} ]";
        }
    }

    struct DeviceKeyboard
    {
        /// <summary> Type of the keyboard. </summary>
        public uint Type;
        /// <summary> Subtype of the keyboard. </summary>
        public uint SubType;
        /// <summary> The scan code mode. </summary>
        public uint KeyboardMode;
        /// <summary> Number of function keys on the keyboard. </summary>
        public uint NumberOfFunctionKeys;
        /// <summary> Number of LED indicators on the keyboard. </summary>
        public uint NumberOfIndicators;
        /// <summary> Total number of keys on the keyboard. </summary>
        public uint NumberOfKeysTotal;

        public override string ToString()
        {
            return $"DeviceInfoKeyboard [ Type: {Type}, SubType: {SubType}, KeyboardMode: {KeyboardMode}, NumberOfFunctionKeys: {NumberOfFunctionKeys}, "
                + $"NumberOfIndicators: {NumberOfIndicators}, NumberOfKeysTotal: {NumberOfKeysTotal} ]";
        }
    }

    struct DeviceHID
    {
        /// <summary> Vendor identifier for the HID. </summary>
        public uint VendorID;
        /// <summary> Product identifier for the HID. </summary>
        public uint ProductID;
        /// <summary> Version number for the device. </summary>
        public uint VersionNumber;
        /// <summary> Top-level collection Usage page for the device. </summary>
        public ushort UsagePage;
        /// <summary> Top-level collection Usage for the device. </summary>
        public ushort Usage;

        public override string ToString()
        {
            return $"HidInfo [ VendorID: {VendorID}, ProductID: {ProductID}, VersionNumber: {VersionNumber}, UsagePage: {UsagePage}, Usage: {Usage} ]";
        }
    }

    struct BroadcastDeviceInterface
    {
        public int DbccSize;
        public BroadcastDeviceType BroadcastDeviceType;
        public int DbccReserved;
        public Guid DbccClassguid;
        public char DbccName;
    }

    [StructLayout(LayoutKind.Sequential)]
    struct Rawinputdevicelist
    {
        public IntPtr hDevice;
        public uint dwType;
    }

    [StructLayout(LayoutKind.Explicit)]
    struct RawData
    {
        [FieldOffset(0)] public RawMouse mouse;
        [FieldOffset(0)] public RawKeyboard keyboard;
        [FieldOffset(0)] public RawHID hid;
    }

    [StructLayout(LayoutKind.Sequential)]
    struct InputData
    {
        /// <summary> 
        /// 64 bit, header size: 24
        /// 32 bit, header size: 16 
        /// </summary>
        public RawInputHeader header;
        /// <summary> Creating the rest in a struct allows the header size to align correctly for 32/64 bit. </summary>
        public RawData data;
    }

    [StructLayout(LayoutKind.Sequential)]
    struct RawInputHeader
    {
        /// <summary> Type of raw input (RIM_TYPEHID 2, RIM_TYPEKEYBOARD 1, RIM_TYPEMOUSE 0). </summary>
        public uint dwType;
        /// <summary> Size in bytes of the entire input packet of data. This includes RAWINPUT plus possible extra input reports in the RAWHID variable length array. </summary>
        public uint dwSize;
        /// <summary> A handle to the device generating the raw input data.  </summary>
        public IntPtr hDevice;
        /// <summary> RIM_INPUT 0 if input occurred while application was in the foreground else RIM_INPUTSINK 1 if it was not. </summary>
        public IntPtr wParam;

        public override string ToString()
        {
            return $"RawInputHeader [ dwType: {dwType}, dwSize: {dwSize}, hDevice: {hDevice}, wParam: {wParam} ]";
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    struct RawHID
    {
        public uint dwSizHid;
        public uint dwCount;
        public byte bRawData;

        public override string ToString()
        {
            return $"Rawhid [ dwSizeHid: {dwSizHid}, dwCount: {dwCount}, bRawData: {bRawData} ]";
        }
    }

    [StructLayout(LayoutKind.Explicit)]
    struct RawMouse
    {
        [FieldOffset(0)] public ushort usFlags;
        [FieldOffset(4)] public uint ulButtons;
        [FieldOffset(4)] public ushort usButtonFlags;
        [FieldOffset(6)] public ushort usButtonData;
        [FieldOffset(8)] public uint ulRawButtons;
        [FieldOffset(12)] public int lLastX;
        [FieldOffset(16)] public int lLastY;
        [FieldOffset(20)] public uint ulExtraInformation;
    }

    [StructLayout(LayoutKind.Sequential)]
    struct RawKeyboard
    {
        /// <summary> Scan code from the key depression. </summary>
        public ushort Makecode;
        /// <summary> One or more of RI_KEY_MAKE, RI_KEY_BREAK, RI_KEY_E0, RI_KEY_E1. </summary>
        public ushort Flags;
        /// <summary> Always 0. </summary>
        private readonly ushort Reserved;
        /// <summary> Virtual Key Code. </summary> 
        public ushort VKey;
        /// <summary> Corresponding Windows message for exmaple (WM_KEYDOWN, WM_SYASKEYDOWN etc). </summary>
        public uint Message;
        /// <summary> The device-specific addition information for the event (seems to always be zero for keyboards). </summary>
        public uint ExtraInformation;

        public override string ToString()
        {
            return $"Rawkeyboard [ Makecode: {Makecode}, Makecode(hex): {Makecode:X}, Flags: {Flags}, Reserved: {Reserved}, VKeyName: {VKey}, Message: {Message}, ExtraInformation {ExtraInformation} ]";
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    struct RawInputDevice
    {
        internal HidUsagePage UsagePage;
        internal HidUsage Usage;
        internal RawInputDeviceFlags Flags;
        internal IntPtr Target;

        public override string ToString()
        {
            return $"{UsagePage} / {Usage}, Flags: {Flags}, Target: {Target}";
        }
    }
}
