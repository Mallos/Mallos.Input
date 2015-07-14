﻿namespace OpenInput.RawInput
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Runtime.InteropServices;
    using System.Windows.Forms;
    
    partial class DeviceService : NativeWindow
    {
        static readonly Guid DeviceInterfaceHid = new Guid("4D1E55B2-F16F-11CF-88CB-001111000030");

        public readonly Dictionary<IntPtr, RawDevice> Devices;

        private readonly IntPtr devNotifyHandle;
        private PreMessageFilter filter;

        private readonly object objectlock = new object();
        private static RawInputData rawBuffer;

        public DeviceService(IntPtr handle)
        {
            AssignHandle(handle);

            this.Devices = new Dictionary<IntPtr, RawDevice>();
            this.devNotifyHandle = RegisterForDeviceNotifications(handle);

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
                                    break;

                                case DeviceType.Keyboard:
                                    {

                                        int virtualKey = rawBuffer.data.keyboard.VKey;
                                        int makeCode = rawBuffer.data.keyboard.Makecode;
                                        int flags = rawBuffer.data.keyboard.Flags;

                                        if (virtualKey == Win32.KEYBOARD_OVERRUN_MAKE_CODE)
                                            return;

                                        var isE0BitSet = ((flags & Win32.RI_KEY_E0) != 0);

                                        RawDevice rawDevice;

                                        if (Devices.ContainsKey(rawBuffer.header.hDevice))
                                        {
                                            lock (objectlock)
                                            {
                                                rawDevice = Devices[rawBuffer.header.hDevice];
                                            }
                                        }
                                        else
                                        {
                                            Debug.WriteLine("Handle: {0} was not in the device list.", rawBuffer.header.hDevice);
                                            return;
                                        }

                                        var isBreakBitSet = ((flags & Win32.RI_KEY_BREAK) != 0);

                                        var KeyPressState = isBreakBitSet ? "BREAK" : "MAKE";
                                        var Message = rawBuffer.data.keyboard.Message;
                                        var VKeyName = KeyMapper.GetKeyName(VirtualKeyCorrection(virtualKey, isE0BitSet, makeCode)).ToUpper();
                                        var VKey = virtualKey;

                                        Console.WriteLine($"RawInput: '{rawDevice.DeviceDescName}', {VKey}, '{VKeyName}', {KeyPressState}");
                                    }
                                    break;

                                case DeviceType.HID:
                                    break;
                            }
                        }
                    } break;

                case Win32.WM_USB_DEVICECHANGE:
                    Debug.WriteLine("RawInput: USB Device Arrival / Removal");
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
