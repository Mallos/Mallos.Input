namespace OpenInput
{
    using System;
    using System.Drawing;
    using System.Windows.Forms;
    using DirectInputSystem = SharpDX.DirectInput.DirectInput;

    // http://www.gamedev.net/blog/233/entry-1567278-reasons-not-to-use-directinput-for-keyboard-input/

    class DeviceService
    {
        public static Lazy<DeviceService> Service = new Lazy<DeviceService>();

        public DirectInputSystem directInput;

        public Rectangle ScreenBounds;

        public DeviceService()
        {
            this.directInput = new DirectInputSystem();

            // Does this create issues for multiple screens?
            this.ScreenBounds = Screen.GetBounds(new System.Drawing.Point(0, 0));
        }
    }
}
