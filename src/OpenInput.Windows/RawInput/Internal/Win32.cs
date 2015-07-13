namespace OpenInput.RawInput
{
    using System;
    using System.Runtime.InteropServices;

    static partial class Win32
    {
        public static int LoWord(int dwValue)
        {
            return (dwValue & 0xFFFF);
        }

        public static int HiWord(Int64 dwValue)
        {
            return (int)(dwValue >> 16) & ~FAPPCOMMANDMASK;
        }

        public static ushort LowWord(uint val)
        {
            return (ushort)val;
        }

        public static ushort HighWord(uint val)
        {
            return (ushort)(val >> 16);
        }

        public static uint BuildWParam(ushort low, ushort high)
        {
            return ((uint)high << 16) | low;
        }

        public const int KEYBOARD_OVERRUN_MAKE_CODE = 0xFF;
        public const int WM_APPCOMMAND = 0x0319;
        private const int FAPPCOMMANDMASK = 0xF000;
        internal const int FAPPCOMMANDMOUSE = 0x8000;
        internal const int FAPPCOMMANDOEM = 0x1000;

        public const int WM_KEYDOWN = 0x0100;
        public const int WM_KEYUP = 0x0101;
        public const int WM_SYSKEYDOWN = 0x0104;
        public const int WM_INPUT = 0x00FF;
        public const int WM_USB_DEVICECHANGE = 0x0219;

        public const int VK_SHIFT = 0x10;

        /// <summary> Key Down </summary>
        public const int RI_KEY_MAKE = 0x00;
        /// <summary> Key Up </summary>
        public const int RI_KEY_BREAK = 0x01;
        /// <summary> Left version of the key </summary>
        public const int RI_KEY_E0 = 0x02;
        /// <summary> Right version of the key. Only seems to be set for the Pause/Break key. </summary>
        public const int RI_KEY_E1 = 0x04;

        public const int VK_CONTROL = 0x11;
        public const int VK_MENU = 0x12;
        public const int VK_ZOOM = 0xFB;
        public const int VK_LSHIFT = 0xA0;
        public const int VK_RSHIFT = 0xA1;
        public const int VK_LCONTROL = 0xA2;
        public const int VK_RCONTROL = 0xA3;
        public const int VK_LMENU = 0xA4;
        public const int VK_RMENU = 0xA5;

        public const int SC_SHIFT_R = 0x36;
        public const int SC_SHIFT_L = 0x2a;
        public const int RIM_INPUT = 0x00;

        [DllImport("User32.dll", SetLastError = true)]
        public static extern int GetRawInputData(IntPtr hRawInput, DataCommand command, [Out] out InputData buffer, [In, Out] ref int size, int cbSizeHeader);

        [DllImport("User32.dll", SetLastError = true)]
        public static extern int GetRawInputData(IntPtr hRawInput, DataCommand command, [Out] IntPtr pData, [In, Out] ref int size, int sizeHeader);

        [DllImport("User32.dll", SetLastError = true)]
        public static extern uint GetRawInputDeviceInfo(IntPtr hDevice, RawInputDeviceInfo command, IntPtr pData, ref uint size);

        [DllImport("user32.dll")]
        public static extern uint GetRawInputDeviceInfo(IntPtr hDevice, uint command, ref DeviceInfo data, ref uint dataSize);

        [DllImport("User32.dll", SetLastError = true)]
        public static extern uint GetRawInputDeviceList(IntPtr pRawInputDeviceList, ref uint numberDevices, uint size);

        [DllImport("User32.dll", SetLastError = true)]
        public static extern bool RegisterRawInputDevices(RawInputDevice[] pRawInputDevice, uint numberDevices, uint size);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr RegisterDeviceNotification(IntPtr hRecipient, IntPtr notificationFilter, DeviceNotification flags);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool UnregisterDeviceNotification(IntPtr handle);

        //public static bool InputInForeground(IntPtr wparam)
        //{
        //    return wparam.ToInt32() == RIM_INPUT;
        //}
    }
}
