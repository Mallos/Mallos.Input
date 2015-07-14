namespace OpenInput.RawInput
{
    using System;
    using System.ComponentModel;
    using System.Globalization;
    using System.IO;
    using System.Runtime.InteropServices;

    static partial class Win32
    {
        // TODO: Remove this method and create some better way of enumerating all devices.

        public static void DeviceAudit()
        {
            var file = new FileStream("DeviceAudit.txt", FileMode.Create, FileAccess.Write);
            var sw = new StreamWriter(file);

            var keyboardNumber = 0;
            uint deviceCount = 0;
            var dwSize = (Marshal.SizeOf(typeof(Rawinputdevicelist)));

            if (GetRawInputDeviceList(IntPtr.Zero, ref deviceCount, (uint)dwSize) == 0)
            {
                var pRawInputDeviceList = Marshal.AllocHGlobal((int)(dwSize * deviceCount));
                GetRawInputDeviceList(pRawInputDeviceList, ref deviceCount, (uint)dwSize);

                for (var i = 0; i < deviceCount; i++)
                {
                    uint pcbSize = 0;

                    // On Window 8 64bit when compiling against .Net > 3.5 using .ToInt32 you will generate an arithmetic overflow. Leave as it is for 32bit/64bit applications
                    var rid = (Rawinputdevicelist)Marshal.PtrToStructure(new IntPtr((pRawInputDeviceList.ToInt64() + (dwSize * i))), typeof(Rawinputdevicelist));

                    GetRawInputDeviceInfo(rid.hDevice, RawInputDeviceInfo.RIDI_DEVICENAME, IntPtr.Zero, ref pcbSize);

                    if (pcbSize <= 0)
                    {
                        sw.WriteLine("pcbSize: " + pcbSize);
                        sw.WriteLine(Marshal.GetLastWin32Error());
                        return;
                    }

                    var size = (uint)Marshal.SizeOf(typeof(DeviceInfo));
                    var di = new DeviceInfo { Size = Marshal.SizeOf(typeof(DeviceInfo)) };

                    if (GetRawInputDeviceInfo(rid.hDevice, (uint)RawInputDeviceInfo.RIDI_DEVICEINFO, ref di, ref size) <= 0)
                    {
                        sw.WriteLine(Marshal.GetLastWin32Error());
                        return;
                    }

                    var pData = Marshal.AllocHGlobal((int)pcbSize);
                    GetRawInputDeviceInfo(rid.hDevice, RawInputDeviceInfo.RIDI_DEVICENAME, pData, ref pcbSize);
                    var deviceName = Marshal.PtrToStringAnsi(pData);

                    switch ((DeviceType)rid.dwType)
                    {
                        case DeviceType.Mouse:
                            break;

                        case DeviceType.HID:
                        case DeviceType.Keyboard:
                            {
                                var deviceDesc = GetDeviceDescription(deviceName);

                                var dInfo = new KeyPressEvent
                                {
                                    DeviceName = Marshal.PtrToStringAnsi(pData),
                                    DeviceHandle = rid.hDevice,
                                    DeviceType = Enum.GetName(typeof(DeviceType), rid.dwType),
                                    Name = deviceDesc,
                                    Source = keyboardNumber++.ToString(CultureInfo.InvariantCulture)
                                };

                                sw.WriteLine(dInfo.ToString());
                                sw.WriteLine(di.ToString());
                                sw.WriteLine(di.Keyboard.ToString());
                                sw.WriteLine(di.HID.ToString());
                                //sw.WriteLine(di.MouseInfo.ToString());
                                sw.WriteLine("=========================================================================================================");
                            } break;
                    }
                    
                    Marshal.FreeHGlobal(pData);
                }

                Marshal.FreeHGlobal(pRawInputDeviceList);

                sw.Flush();
                sw.Close();
                file.Close();

                return;
            }

            throw new Win32Exception(Marshal.GetLastWin32Error());
        }
        
        public static string GetDeviceDescription(string device)
        {
            string deviceDesc;
            try
            {
                var deviceKey = RegistryAccess.GetDeviceKey(device);
                deviceDesc = deviceKey.GetValue("DeviceDesc").ToString();
                deviceDesc = deviceDesc.Substring(deviceDesc.IndexOf(';') + 1);
            }
            catch (Exception)
            {
                deviceDesc = "Device is malformed unable to look up in the registry";
            }

            //var deviceClass = RegistryAccess.GetClassType(deviceKey.GetValue("ClassGUID").ToString());
            //isKeyboard = deviceClass.ToUpper().Equals( "KEYBOARD" );

            return deviceDesc;
        }
    }
}
