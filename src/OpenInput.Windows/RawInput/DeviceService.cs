namespace OpenInput.RawInput
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Runtime.InteropServices;
    using System.Windows.Forms;

    // TODO: Implement GamePad support
    //       Unfortunately there is no reference ón how to do this on microsofts page
    //       https://msdn.microsoft.com/en-us/library/windows/desktop/ms645546(v=vs.85).aspx

    partial class DeviceService : NativeWindow
    {
        private readonly object objectlock = new object();
        private static RawInputData rawBuffer;

        //public readonly Dictionary<IntPtr, RawDeviceInfo> Devices;
        public readonly HashSet<OpenInput.Keys> Keys;

        public MouseState MouseState => mouseState;
        private MouseState mouseState;

        private readonly IntPtr devNotifyHandle;
        private PreMessageFilter filter;

        public DeviceService(IntPtr handle)
        {
            AssignHandle(handle);

            //this.Devices = new Dictionary<IntPtr, RawDeviceInfo>();
            this.Keys = new HashSet<OpenInput.Keys>();
            this.mouseState = new MouseState();
            this.devNotifyHandle = RegisterForDeviceNotifications(handle);

            FindDevices();
        }

        ~DeviceService()
        {
            Win32.UnregisterDeviceNotification(devNotifyHandle);
            RemoveMessageFilter();
        }

        #region MessageFilter
        public void AddMessageFilter()
        {
            if (filter != null)
                return;

            filter = new PreMessageFilter();
            Application.AddMessageFilter(filter);
        }

        private void RemoveMessageFilter()
        {
            if (filter != null)
                return;

            Application.RemoveMessageFilter(filter);
        }
        #endregion

        protected override void WndProc(ref Message message)
        {
            // TODO: Support MSG_GETRIUFFER
            //       This could be useful on the inital start
            //       otherwise we are keeping track of all the keys

            switch (message.Msg)
            {
                case Win32.WM_INPUT:
                    {
                        var dwSize = 0;
                        var hdevice = message.LParam;
                        Win32.GetRawInputData(hdevice, DataCommand.RID_INPUT, IntPtr.Zero, ref dwSize, Marshal.SizeOf(typeof(RawInputHeader)));
                        if (dwSize != Win32.GetRawInputData(hdevice, DataCommand.RID_INPUT, out rawBuffer, ref dwSize, Marshal.SizeOf(typeof(RawInputHeader))))
                        {
                            Debug.WriteLine("RawInput: Failed to get the buffer.");
                        }
                        else
                        {
                            switch ((DeviceType)rawBuffer.header.dwType)
                            {
                                case DeviceType.Mouse:
                                    OnMouseEvent();
                                    break;

                                case DeviceType.Keyboard:
                                    OnKeyboardEvent();
                                    break;

                                case DeviceType.HID:
                                    break;
                            }
                        }
                    }
                    break;

                case Win32.WM_USB_DEVICECHANGE:
                    Debug.WriteLine("RawInput: USB Device Arrival / Removal");
                    // TODO: Refresh all devices and call device connect / disconnect events
                    break;
            }

            base.WndProc(ref message);
        }

        #region WndProc Messages
        private void OnMouseEvent()
        {
            switch ((MouseFlags)rawBuffer.data.mouse.usFlags)
            {
                case MouseFlags.VirtualDesktop:
                    break;

                case MouseFlags.AttributesChanged:
                    break;

                case MouseFlags.MoveRelative:
                    {
                        // TODO: Get the mouse initial position first
                        //       How does MoveAbsolute work? When does it get called and how?

                        this.mouseState.X += rawBuffer.data.mouse.lLastX;
                        this.mouseState.Y += rawBuffer.data.mouse.lLastY;

                        var screenBounds = Screen.GetBounds(new System.Drawing.Point(0, 0));

                        if (this.mouseState.X < screenBounds.X) this.mouseState.X = screenBounds.X;
                        if (this.mouseState.Y < screenBounds.Y) this.mouseState.Y = screenBounds.Y;
                        if (this.mouseState.X > screenBounds.Width) this.mouseState.X = screenBounds.Width;
                        if (this.mouseState.Y > screenBounds.Height) this.mouseState.Y = screenBounds.Height;
                    }
                    break;

                case MouseFlags.MoveAbsolute:
                    //mouseState.X = rawBuffer.data.mouse.lLastX;
                    //mouseState.Y = rawBuffer.data.mouse.lLastY;
                    break;
            }

            var buttons = (MouseButtonsFlags)rawBuffer.data.mouse.ulButtons;
            mouseState.LeftButton = (buttons & MouseButtonsFlags.LeftButtonDown) == MouseButtonsFlags.LeftButtonDown;
            mouseState.MiddleButton = (buttons & MouseButtonsFlags.MiddleButtonDown) == MouseButtonsFlags.MiddleButtonDown;
            mouseState.RightButton = (buttons & MouseButtonsFlags.RightButtonDown) == MouseButtonsFlags.RightButtonDown;
            
            // TODO: Add support for XButton1 & 2
            mouseState.XButton1 = false; 
            mouseState.XButton2 = false;
        }

        private void OnKeyboardEvent()
        {
            int virtualKey = rawBuffer.data.keyboard.VKey;
            int makeCode = rawBuffer.data.keyboard.Makecode;
            int flags = rawBuffer.data.keyboard.Flags;

            if (virtualKey == Win32.KEYBOARD_OVERRUN_MAKE_CODE)
                return;

            var isE0BitSet = ((flags & Win32.RI_KEY_E0) != 0);

            //RawDeviceInfo rawDevice;

            //if (Devices.ContainsKey(rawBuffer.header.hDevice))
            //{
            //    lock (objectlock)
            //    {
            //        rawDevice = Devices[rawBuffer.header.hDevice];
            //    }
            //}
            //else
            //{
            //    Debug.WriteLine("RawInput: handle '{0}' was not in the device list.", rawBuffer.header.hDevice);
            //    return;
            //}

            var isBreakBitSet = ((flags & Win32.RI_KEY_BREAK) != 0);

            var KeyPressState = isBreakBitSet ? "BREAK" : "MAKE";
            var Message = rawBuffer.data.keyboard.Message;
            var VKeyName = KeyMapper.GetKeyName(VirtualKeyCorrection(virtualKey, isE0BitSet, makeCode)).ToUpper();
            var VKey = virtualKey;

            if (isBreakBitSet)
            {
                Keys.Remove(KeyMapper.ToKey(VKey));
            }
            else
            {
                Keys.Add(KeyMapper.ToKey(VKey));
            }

            //Console.WriteLine($"RawInput: '{rawDevice.DeviceDescName}', {VKey}, '{VKeyName}', {KeyPressState}");
        }
        #endregion

        static IntPtr RegisterForDeviceNotifications(IntPtr parent)
        {
            var usbNotifyHandle = IntPtr.Zero;
            var bdi = new BroadcastDeviceInterface();
            bdi.DbccSize = Marshal.SizeOf(bdi);
            bdi.BroadcastDeviceType = BroadcastDeviceType.DBT_DEVTYP_DEVICEINTERFACE;
            bdi.DbccClassguid = DeviceInterfaceHid;

            var mem = IntPtr.Zero;
            try
            {
                mem = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(BroadcastDeviceInterface)));
                Marshal.StructureToPtr(bdi, mem, false);
                usbNotifyHandle = Win32.RegisterDeviceNotification(parent, mem, DeviceNotification.DEVICE_NOTIFY_WINDOW_HANDLE);
            }
            catch (Exception e)
            {
                Debug.Print("RawInput: Registration for device notifications Failed. Error: {0}", Marshal.GetLastWin32Error());
                Debug.Print(e.StackTrace);
            }
            finally
            {
                Marshal.FreeHGlobal(mem);
            }

            if (usbNotifyHandle == IntPtr.Zero)
            {
                Debug.Print("RawInput: Registration for device notifications Failed. Error: {0}", Marshal.GetLastWin32Error());
            }

            return usbNotifyHandle;
        }

        static int VirtualKeyCorrection(int virtualKey, bool isE0BitSet, int makeCode)
        {
            var correctedVKey = virtualKey;

            if (rawBuffer.header.hDevice == IntPtr.Zero)
            {
                // When hDevice is 0 and the vkey is VK_CONTROL indicates the ZOOM key
                if (rawBuffer.data.keyboard.VKey == Win32.VK_CONTROL)
                {
                    correctedVKey = Win32.VK_ZOOM;
                }
            }
            else
            {
                switch (virtualKey)
                {
                    // Right-hand CTRL and ALT have their e0 bit set 
                    case Win32.VK_CONTROL:
                        correctedVKey = isE0BitSet ? Win32.VK_RCONTROL : Win32.VK_LCONTROL;
                        break;
                    case Win32.VK_MENU:
                        correctedVKey = isE0BitSet ? Win32.VK_RMENU : Win32.VK_LMENU;
                        break;
                    case Win32.VK_SHIFT:
                        correctedVKey = makeCode == Win32.SC_SHIFT_R ? Win32.VK_RSHIFT : Win32.VK_LSHIFT;
                        break;
                    default:
                        correctedVKey = virtualKey;
                        break;
                }
            }

            return correctedVKey;
        }
    }
}
