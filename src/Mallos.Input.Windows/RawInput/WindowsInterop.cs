namespace Mallos.Input
{
    using System;
    using System.Runtime.InteropServices;
    using System.Text;

    /// <summary>
    /// The set of valid MapTypes used in MapVirtualKey
    /// </summary>
    public enum MapVirtualKeyMapTypes : uint
    {
        /// <summary>
        /// uCode is a virtual-key code and is translated into a scan code.
        /// If it is a virtual-key code that does not distinguish between left- and
        /// right-hand keys, the left-hand scan code is returned.
        /// If there is no translation, the function returns 0.
        /// </summary>
        MAPVK_VK_TO_VSC = 0x00,

        /// <summary>
        /// uCode is a scan code and is translated into a virtual-key code that
        /// does not distinguish between left- and right-hand keys. If there is no
        /// translation, the function returns 0.
        /// </summary>
        MAPVK_VSC_TO_VK = 0x01,

        /// <summary>
        /// uCode is a virtual-key code and is translated into an unshifted
        /// character value in the low-order word of the return value. Dead keys (diacritics)
        /// are indicated by setting the top bit of the return value. If there is no
        /// translation, the function returns 0.
        /// </summary>
        MAPVK_VK_TO_CHAR = 0x02,

        /// <summary>
        /// Windows NT/2000/XP: uCode is a scan code and is translated into a
        /// virtual-key code that distinguishes between left- and right-hand keys. If
        /// there is no translation, the function returns 0.
        /// </summary>
        MAPVK_VSC_TO_VK_EX = 0x03,

        /// <summary>
        /// Not currently documented
        /// </summary>
        MAPVK_VK_TO_VSC_EX = 0x04
    }

    static partial class WindowsInterop
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

        public static string GetKeyNameText(int keycode, bool isE0BitSet)
        {
            uint key = ((uint)keycode << 16);

            if (isE0BitSet)
            {
                key |= 1 << 24;
            }

            var stringBuilder = new System.Text.StringBuilder();
            WindowsInterop.GetKeyNameText((int)key, stringBuilder, 512);

            return stringBuilder.ToString();
        }

        public const int KEYBOARD_OVERRUN_MAKE_CODE = 0xFF;
        public const int WM_APPCOMMAND = 0x0319;
        public const int FAPPCOMMANDMASK = 0xF000;
        public const int FAPPCOMMANDMOUSE = 0x8000;
        public const int FAPPCOMMANDOEM = 0x1000;

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

        [DllImport("user32.dll")]
        public static extern uint MapVirtualKey(uint uCode, uint uMapType);

        [DllImport("user32.dll")]
        public static extern int MapVirtualKey(uint uCode, MapVirtualKeyMapTypes uMapType);

        [DllImport("user32.dll")]
        public static extern uint MapVirtualKeyEx(uint uCode, uint uMapType, IntPtr dwhkl);

        [DllImport("user32.dll")]
        public static extern uint MapVirtualKeyEx(uint uCode, MapVirtualKeyMapTypes uMapType, IntPtr dwhkl);

        [DllImport("user32.dll")]
        public static extern int GetKeyNameText(int lParam, [Out] StringBuilder lpString, int nSize);
    }
}
