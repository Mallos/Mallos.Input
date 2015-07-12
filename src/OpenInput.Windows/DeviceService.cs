namespace OpenInput
{
    using SharpDX.DirectInput;
    using System;

    class DeviceService
    {
        public static Lazy<DeviceService> Service = new Lazy<DeviceService>();

        public DirectInput directInput;

        public DeviceService()
        {
            this.directInput = new DirectInput();
        }
    }
}
