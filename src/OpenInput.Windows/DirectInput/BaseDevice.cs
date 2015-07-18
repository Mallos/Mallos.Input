namespace OpenInput.DirectInput
{
    using System.Drawing;
    using System.Windows.Forms;
    using DirectInputSystem = SharpDX.DirectInput.DirectInput;

    public abstract class BaseDevice
    {
        internal static DirectInputSystem directInput;
        internal static Rectangle screenBounds;

        public BaseDevice()
        {
            if (directInput == null)
                directInput = new DirectInputSystem();

            // Does this create issues for multiple screens?
            if (screenBounds == null)
                screenBounds = Screen.GetBounds(new Point(0, 0));
        }
    }
}
