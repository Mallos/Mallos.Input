namespace OpenInput.RawInput
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Runtime.InteropServices;
    using System.Windows.Forms;

    partial class DeviceService : NativeWindow
    {
        static readonly Guid DeviceInterfaceHid = new Guid("4D1E55B2-F16F-11CF-88CB-001111000030");
        public static Lazy<DeviceService> Service = new Lazy<DeviceService>();

        private readonly IntPtr devNotifyHandle;
        private PreMessageFilter filter;

        readonly object objectlock = new object();
        private static InputData rawBuffer;

        public event DeviceEventHandler KeyPressed;

        public DeviceService(IntPtr parentHandle)
        {
            AssignHandle(parentHandle);

            this.Devices = new Dictionary<IntPtr, KeyPressEvent>();
            this.devNotifyHandle = RegisterForDeviceNotifications(parentHandle);

            FindDevices();

            //Win32.DeviceAudit();
        }

        ~DeviceService()
        {
            Win32.UnregisterDeviceNotification(devNotifyHandle);
            RemoveMessageFilter();
        }

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

        protected override void WndProc(ref Message message)
        {
            switch (message.Msg)
            {
                case Win32.WM_INPUT:
                    //if (Devices.Count > 0)
                    {
                        var hdevice = message.LParam;
                        //Debug.WriteLine(_rawBuffer.data.keyboard.ToString());
                        //Debug.WriteLine(_rawBuffer.data.hid.ToString());
                        //Debug.WriteLine(_rawBuffer.header.ToString());

                        var dwSize = 0;
                        Win32.GetRawInputData(hdevice, DataCommand.RID_INPUT, IntPtr.Zero, ref dwSize, Marshal.SizeOf(typeof(Rawinputheader)));

                        if (dwSize != Win32.GetRawInputData(hdevice, DataCommand.RID_INPUT, out rawBuffer, ref dwSize, Marshal.SizeOf(typeof(Rawinputheader))))
                        {
                            Debug.WriteLine("Error getting the rawinput buffer");
                            return;
                        }

                        int virtualKey = rawBuffer.data.keyboard.VKey;
                        int makeCode = rawBuffer.data.keyboard.Makecode;
                        int flags = rawBuffer.data.keyboard.Flags;

                        if (virtualKey == Win32.KEYBOARD_OVERRUN_MAKE_CODE) return;

                        var isE0BitSet = ((flags & Win32.RI_KEY_E0) != 0);

                        KeyPressEvent keyPressEvent;

                        if (Devices.ContainsKey(rawBuffer.header.hDevice))
                        {
                            lock (objectlock)
                            {
                                keyPressEvent = Devices[rawBuffer.header.hDevice];
                            }
                        }
                        else
                        {
                            Debug.WriteLine("Handle: {0} was not in the device list.", rawBuffer.header.hDevice);
                            return;
                        }

                        var isBreakBitSet = ((flags & Win32.RI_KEY_BREAK) != 0);

                        keyPressEvent.KeyPressState = isBreakBitSet ? "BREAK" : "MAKE";
                        keyPressEvent.Message = rawBuffer.data.keyboard.Message;
                        keyPressEvent.VKeyName = KeyMapper.GetKeyName(VirtualKeyCorrection(virtualKey, isE0BitSet, makeCode)).ToUpper();
                        keyPressEvent.VKey = virtualKey;

                        if (KeyPressed != null)
                        {
                            KeyPressed(this, new RawInputEventArg(keyPressEvent));
                        }
                    }
                    break;

                case Win32.WM_USB_DEVICECHANGE:
                    Debug.WriteLine("USB Device Arrival / Removal");
                    // TODO: Call event
                    break;
            }

            base.WndProc(ref message);
        }
        
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
                Debug.Print("Registration for device notifications Failed. Error: {0}", Marshal.GetLastWin32Error());
                Debug.Print(e.StackTrace);
            }
            finally
            {
                Marshal.FreeHGlobal(mem);
            }

            if (usbNotifyHandle == IntPtr.Zero)
            {
                Debug.Print("Registration for device notifications Failed. Error: {0}", Marshal.GetLastWin32Error());
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
