namespace OpenInput.RawInput
{
    using System;

    enum DataCommand : uint
    {
        /// <summary> Get the header information from the RAWINPUT structure. </summary>
        RID_HEADER = 0x10000005, 
        /// <summary> Get the raw data from the RAWINPUT structure. </summary>
        RID_INPUT = 0x10000003
    }

    enum DeviceType : uint
    {
        Mouse = 0,
        Keyboard = 1,
        HID = 2,
    }

    enum RawInputDeviceInfo : uint
    {
        RIDI_DEVICENAME = 0x20000007,
        RIDI_DEVICEINFO = 0x2000000b,
        PREPARSEDDATA = 0x20000005
    }

    enum BroadcastDeviceType
    {
        DBT_DEVTYP_OEM = 0,
        DBT_DEVTYP_DEVNODE = 1,
        DBT_DEVTYP_VOLUME = 2,
        DBT_DEVTYP_PORT = 3,
        DBT_DEVTYP_NET = 4,
        DBT_DEVTYP_DEVICEINTERFACE = 5,
        DBT_DEVTYP_HANDLE = 6,
    }

    enum DeviceNotification
    {
        /// <summary> The hRecipient parameter is a window handle. </summary>
        DEVICE_NOTIFY_WINDOW_HANDLE = 0x00000000,
        /// <summary> The hRecipient parameter is a service status handle. </summary>
        DEVICE_NOTIFY_SERVICE_HANDLE = 0x00000001,
        /// <summary>
        /// Notifies the recipient of device interface events for all device interface classes. (The dbcc_classguid member is ignored.)
        /// This value can be used only if the dbch_devicetype member is DBT_DEVTYP_DEVICEINTERFACE.
        /// </summary>
        DEVICE_NOTIFY_ALL_INTERFACE_CLASSES = 0x00000004
    }

    [Flags]
    enum RawInputDeviceFlags
    {
        /// <summary> No flags. </summary>
        NONE = 0,
        /// <summary> Removes the top level collection from the inclusion list. This tells the operating system to stop reading from a device which matches the top level collection. </summary>
        REMOVE = 0x00000001,
        /// <summary> Specifies the top level collections to exclude when reading a complete usage page. This flag only affects a TLC whose usage page is already specified with PageOnly. </summary>
        EXCLUDE = 0x00000010,
        /// <summary> Specifies all devices whose top level collection is from the specified UsagePage. Note that Usage must be zero. To exclude a particular top level collection, use Exclude. </summary>
        PAGEONLY = 0x00000020,
        /// <summary> Prevents any devices specified by UsagePage or Usage from generating legacy messages. This is only for the mouse and keyboard. </summary>
        NOLEGACY = 0x00000030,
        /// <summary> Enables the caller to receive the input even when the caller is not in the foreground. Note that WindowHandle must be specified. </summary>
        INPUTSINK = 0x00000100,
        /// <summary> Mouse button click does not activate the other window. </summary>
        CAPTUREMOUSE = 0x00000200, 
        /// <summary> Application-defined keyboard device hotkeys are not handled. However, the system hotkeys; for example, ALT+TAB and CTRL+ALT+DEL, are still handled. By default, all keyboard hotkeys are handled. NoHotKeys can be specified even if NoLegacy is not specified and WindowHandle is NULL. </summary>
        NOHOTKEYS = 0x00000200,
        /// <summary> Application keys are handled.  NoLegacy must be specified. Keyboard only. </summary>
        APPKEYS = 0x00000400,
        /// <summary>
        /// Enables the caller to receive input in the background only if the foreground application does not process it. 
        /// In other words, if the foreground application is not registered for raw input, then the background application that is registered will receive the input.
        /// </summary>
        EXINPUTSINK = 0x00001000,
        /// <summary>  </summary>
        DEVNOTIFY = 0x00002000
    }

    enum HidUsagePage : ushort
    {
        /// <summary> Unknown usage page </summary>
        UNDEFINED = 0x00,
        /// <summary> Generic desktop controls </summary>
        GENERIC = 0x01,
        /// <summary> Simulation controls </summary>
        SIMULATION = 0x02,
        /// <summary> Virtual reality controls </summary>
        VR = 0x03,
        /// <summary> Sports controls </summary>
        SPORT = 0x04,
        /// <summary> Games controls </summary>
        GAME = 0x05,
        /// <summary> Keyboard controls </summary>
        KEYBOARD = 0x07,
    }

    enum HidUsage : ushort
    {
        /// <summary> Unknown usage </summary>
        Undefined = 0x00,
        /// <summary> Pointer </summary>
        Pointer = 0x01,
        /// <summary> Mouse </summary>
        Mouse = 0x02,
        /// <summary> Joystick </summary>
        Joystick = 0x04,
        /// <summary> GamePad </summary>
        Gamepad = 0x05,
        /// <summary> Keyboard </summary>
        Keyboard = 0x06,
        /// <summary> Keypad </summary>
        Keypad = 0x07,
        /// <summary> Muilt-axis Controller </summary>
        SystemControl = 0x80,
        /// <summary> Tablet PC controls </summary>
        Tablet = 0x80,
        /// <summary> Consumer </summary>
        Consumer = 0x0C
    }
}
