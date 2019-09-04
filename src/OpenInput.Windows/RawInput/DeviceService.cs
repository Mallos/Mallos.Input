namespace OpenInput.RawInput
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Runtime.InteropServices;
    using System.Windows.Forms;

    partial class DeviceService : NativeWindow
    {
        private readonly object objectlock = new object();
        private static RawInputData rawBuffer;

        public readonly HashSet<OpenInput.Keys> Keys;

        public MouseState MouseState;

        private MouseButtons mouseButtons = MouseButtons.None;
        private int realMouseX = 0, realMouseY = 0;
        private int realScrollWheelValue = 0;

        private int MouseX, MouseY;

        private readonly IntPtr devNotifyHandle;
        private PreMessageFilter filter;

        public DeviceService(IntPtr handle)
        {
            AssignHandle(handle);

            this.Keys = new HashSet<OpenInput.Keys>();
            this.MouseState = MouseState.Empty;
            this.devNotifyHandle = RegisterForDeviceNotifications(handle);

            FindDevices();

            // Set the mouse initial position
            MouseX = Cursor.Position.X;
            MouseY = Cursor.Position.Y;
            
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
                        this.MouseX += rawBuffer.data.mouse.lLastX;
                        this.MouseY += rawBuffer.data.mouse.lLastY;

                        var screenBounds = Screen.GetBounds(new System.Drawing.Point(0, 0));

                        if (this.MouseX < screenBounds.X) this.MouseX = screenBounds.X;
                        if (this.MouseY < screenBounds.Y) this.MouseY = screenBounds.Y;
                        if (this.MouseX > screenBounds.Width)  this.MouseX = screenBounds.Width;
                        if (this.MouseY > screenBounds.Height) this.MouseY = screenBounds.Height;

                        // Set mouse position relative to window
                        RECT rect = new RECT();
                        if (WindowsInterop.GetWindowRect(Handle, ref rect))
                        {
                            this.realMouseX = this.MouseX - rect.Left;
                            this.realMouseY = this.MouseY - rect.Top;
                        }
                        else
                        {
                            this.realMouseX = this.MouseX;
                            this.realMouseY = this.MouseY;
                        }

                    } break;

                case MouseFlags.MoveAbsolute:
                    //mouseState.X = rawBuffer.data.mouse.lLastX;
                    //mouseState.Y = rawBuffer.data.mouse.lLastY;
                    break;
            }

            switch ((MouseButtonsFlags)rawBuffer.data.mouse.usButtonFlags)
            {
                case MouseButtonsFlags.MouseWheel:
                    this.realScrollWheelValue += (rawBuffer.data.mouse.usButtonData == 120) ? 1 : -1;
                    break;

                case MouseButtonsFlags.LeftButtonDown: this.mouseButtons |= MouseButtons.Left; break;
                case MouseButtonsFlags.LeftButtonUp: this.mouseButtons &= ~MouseButtons.Left; break;

                case MouseButtonsFlags.MiddleButtonDown: this.mouseButtons |= MouseButtons.Middle; break;
                case MouseButtonsFlags.MiddleButtonUp: this.mouseButtons &= ~MouseButtons.Middle; break;

                case MouseButtonsFlags.RightButtonDown: this.mouseButtons |= MouseButtons.Right; break;
                case MouseButtonsFlags.RightButtonUp: this.mouseButtons &= ~MouseButtons.Right; break;

                case MouseButtonsFlags.Button4Down: this.mouseButtons |= MouseButtons.XButton1; break;
                case MouseButtonsFlags.Button4Up: this.mouseButtons &= ~MouseButtons.XButton1; break;

                case MouseButtonsFlags.Button5Down: this.mouseButtons |= MouseButtons.XButton2; break;
                case MouseButtonsFlags.Button5Up: this.mouseButtons &= ~MouseButtons.XButton2; break;
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
                //Console.WriteLine($"RawInput: {WindowsInterop.GetKeyNameText(rawBuffer.data.keyboard.Makecode, isE0BitSet)}");
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
                Debug.WriteLine($"RawInput: Registration for device notifications Failed. Error: { Marshal.GetLastWin32Error() }");
                Debug.WriteLine(e.StackTrace);
            }
            finally
            {
                Marshal.FreeHGlobal(mem);
            }

            if (usbNotifyHandle == IntPtr.Zero)
            {
                Debug.WriteLine($"RawInput: Registration for device notifications Failed. Error: { Marshal.GetLastWin32Error() }");
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
