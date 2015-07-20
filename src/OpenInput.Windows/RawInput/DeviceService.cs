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

        public readonly HashSet<OpenInput.Keys> Keys;

        public MouseState MouseState;

        private readonly IntPtr devNotifyHandle;
        private PreMessageFilter filter;

        public DeviceService(IntPtr handle)
        {
            AssignHandle(handle);

            this.Keys = new HashSet<OpenInput.Keys>();
            this.MouseState = new MouseState();
            this.devNotifyHandle = RegisterForDeviceNotifications(handle);

            FindDevices();

            // Set the mouse initial position
            MouseState.X = Cursor.Position.X;
            MouseState.Y = Cursor.Position.Y;
            
            // Register devices
            var rids = new RawInputDevice[]
            {
                new RawInputDevice // Mouse
                {
                    UsagePage = HidUsagePage.GENERIC,
                    Usage = HidUsage.Mouse,
                    Flags = RawInputDeviceFlags.INPUTSINK | RawInputDeviceFlags.DEVNOTIFY,
                    Target = handle
                },
                new RawInputDevice // Keyboard
                {
                    UsagePage = HidUsagePage.GENERIC,
                    Usage = HidUsage.Keyboard,
                    Flags = RawInputDeviceFlags.INPUTSINK | RawInputDeviceFlags.DEVNOTIFY,
                    Target = handle
                },
            };

            if (!WindowsInterop.RegisterRawInputDevices(rids, (uint)rids.Length, (uint)Marshal.SizeOf(rids[0])))
                throw new ApplicationException("RawInput: Failed to register devices.");
        }

        ~DeviceService()
        {
            WindowsInterop.UnregisterDeviceNotification(devNotifyHandle);
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
            switch (message.Msg)
            {
                case WindowsInterop.WM_INPUT:
                    {
                        var dwSize = 0;
                        var hdevice = message.LParam;
                        WindowsInterop.GetRawInputData(hdevice, DataCommand.RID_INPUT, IntPtr.Zero, ref dwSize, Marshal.SizeOf(typeof(RawInputHeader)));
                        if (dwSize != WindowsInterop.GetRawInputData(hdevice, DataCommand.RID_INPUT, out rawBuffer, ref dwSize, Marshal.SizeOf(typeof(RawInputHeader))))
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

                case WindowsInterop.WM_USB_DEVICECHANGE:
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
                        this.MouseState.X += rawBuffer.data.mouse.lLastX;
                        this.MouseState.Y += rawBuffer.data.mouse.lLastY;

                        var screenBounds = Screen.GetBounds(new System.Drawing.Point(0, 0));

                        if (this.MouseState.X < screenBounds.X) this.MouseState.X = screenBounds.X;
                        if (this.MouseState.Y < screenBounds.Y) this.MouseState.Y = screenBounds.Y;
                        if (this.MouseState.X > screenBounds.Width) this.MouseState.X = screenBounds.Width;
                        if (this.MouseState.Y > screenBounds.Height) this.MouseState.Y = screenBounds.Height;
                    } break;

                case MouseFlags.MoveAbsolute:
                    //mouseState.X = rawBuffer.data.mouse.lLastX;
                    //mouseState.Y = rawBuffer.data.mouse.lLastY;
                    break;
            }

            switch ((MouseButtonsFlags)rawBuffer.data.mouse.usButtonFlags)
            {
                case MouseButtonsFlags.MouseWheel:
                    this.MouseState.ScrollWheelValue += (rawBuffer.data.mouse.usButtonData == 120) ? 1 : -1;
                    break;

                case MouseButtonsFlags.LeftButtonDown: this.MouseState.LeftButton = true; break;
                case MouseButtonsFlags.LeftButtonUp: this.MouseState.LeftButton = false; break;

                case MouseButtonsFlags.MiddleButtonDown: this.MouseState.MiddleButton = true; break;
                case MouseButtonsFlags.MiddleButtonUp: this.MouseState.MiddleButton = false; break;

                case MouseButtonsFlags.RightButtonDown: this.MouseState.RightButton = true; break;
                case MouseButtonsFlags.RightButtonUp: this.MouseState.RightButton = false; break;

                case MouseButtonsFlags.Button4Down: this.MouseState.XButton1 = true; break;
                case MouseButtonsFlags.Button4Up: this.MouseState.XButton1 = false; break;

                case MouseButtonsFlags.Button5Down: this.MouseState.XButton2 = true; break;
                case MouseButtonsFlags.Button5Up: this.MouseState.XButton2 = false; break;
            }
        }

        private void OnKeyboardEvent()
        {
            // TODO: Support MSG_GETRIUFFER
            //       This could be useful on the inital start
            //       otherwise we are keeping track of all the keys

            int virtualKey = rawBuffer.data.keyboard.VKey;
            int makeCode = rawBuffer.data.keyboard.Makecode;
            int flags = rawBuffer.data.keyboard.Flags;

            if (virtualKey == WindowsInterop.KEYBOARD_OVERRUN_MAKE_CODE)
                return;

            var isE0BitSet = ((flags & WindowsInterop.RI_KEY_E0) != 0);
            var isBreakBitSet = ((flags & WindowsInterop.RI_KEY_BREAK) != 0);

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
                // NOTE: Here are some more code on this
                // https://molecularmusings.wordpress.com/2011/09/05/properly-handling-keyboard-input/
                Console.WriteLine($"RawInput: {WindowsInterop.GetKeyNameText(rawBuffer.data.keyboard.Makecode, isE0BitSet)}");
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
                usbNotifyHandle = WindowsInterop.RegisterDeviceNotification(parent, mem, DeviceNotification.DEVICE_NOTIFY_WINDOW_HANDLE);
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
                if (rawBuffer.data.keyboard.VKey == WindowsInterop.VK_CONTROL)
                {
                    correctedVKey = WindowsInterop.VK_ZOOM;
                }
            }
            else
            {
                switch (virtualKey)
                {
                    // Right-hand CTRL and ALT have their e0 bit set 
                    case WindowsInterop.VK_CONTROL:
                        correctedVKey = isE0BitSet ? WindowsInterop.VK_RCONTROL : WindowsInterop.VK_LCONTROL;
                        break;

                    case WindowsInterop.VK_MENU:
                        correctedVKey = isE0BitSet ? WindowsInterop.VK_RMENU : WindowsInterop.VK_LMENU;
                        break;

                    case WindowsInterop.VK_SHIFT:
                        correctedVKey = makeCode == WindowsInterop.SC_SHIFT_R ? WindowsInterop.VK_RSHIFT : WindowsInterop.VK_LSHIFT;
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
