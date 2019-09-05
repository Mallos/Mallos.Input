namespace Mallos.Input.RawInput
{
    using System;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Runtime.InteropServices;

    partial class DeviceService
    {
        static readonly Guid DeviceInterfaceHid = new Guid("4D1E55B2-F16F-11CF-88CB-001111000030");

        public ushort KeyboardCount = 0;
        public string KeyboardNames = string.Empty;

        public ushort MouseCount = 0;
        public string MouseNames = string.Empty;

        private uint deviceCount = 0;

        private void FindDevices()
        {
            lock (objectlock)
            {
                this.KeyboardCount = 0;
                this.KeyboardNames = string.Empty;

                this.MouseCount = 0;
                this.MouseNames = string.Empty;
                
                var dwSize = (Marshal.SizeOf(typeof(RawInputDeviceList)));
                if (WindowsInterop.GetRawInputDeviceList(IntPtr.Zero, ref deviceCount, (uint)dwSize) == 0)
                {
                    var rawInputDeviceList = Marshal.AllocHGlobal((int)(dwSize * deviceCount));
                    WindowsInterop.GetRawInputDeviceList(rawInputDeviceList, ref deviceCount, (uint)dwSize);

                    for (var i = 0; i < deviceCount; i++)
                    {
                        // On Window 8 64bit when compiling against .Net > 3.5 using .ToInt32 you will generate an arithmetic overflow. Leave as it is for 32bit/64bit applications
                        var rid = (RawInputDeviceList)Marshal.PtrToStructure(new IntPtr((rawInputDeviceList.ToInt64() + (dwSize * i))), typeof(RawInputDeviceList));

                        uint pcbSize = 0;
                        WindowsInterop.GetRawInputDeviceInfo(rid.hDevice, RawInputDeviceInfo.RIDI_DEVICENAME, IntPtr.Zero, ref pcbSize);
                        if (pcbSize <= 0)
                            continue;

                        var pData = Marshal.AllocHGlobal((int)pcbSize);
                        WindowsInterop.GetRawInputDeviceInfo(rid.hDevice, RawInputDeviceInfo.RIDI_DEVICENAME, pData, ref pcbSize);

                        var deviceName = Marshal.PtrToStringAnsi(pData);
                        var deviceDesc = WindowsInterop.GetDeviceDescription(deviceName);

                        Debug.WriteLine($"RawInput: {deviceName}, {deviceDesc}, {Enum.GetName(typeof(DeviceType), rid.dwType)}");

                        switch ((DeviceType)rid.dwType)
                        {
                            // TODO: I should change the ", " addition to use the counts instead

                            case DeviceType.Mouse:
                                this.MouseCount++;
                                this.MouseNames += ((this.MouseNames == string.Empty) ? "" : ", ") + deviceDesc;
                                break;

                            case DeviceType.Keyboard:
                                this.KeyboardCount++;
                                this.KeyboardNames += ((this.KeyboardNames == string.Empty) ? "" : ", ") + deviceDesc;
                                break;

                            case DeviceType.HID:
                                break;
                        }

                        Marshal.FreeHGlobal(pData);
                    }

                    Marshal.FreeHGlobal(rawInputDeviceList);
                }
                else
                {
                    throw new Win32Exception("RawInput: " + Marshal.GetLastWin32Error());
                }
            }
        }
    }
}
