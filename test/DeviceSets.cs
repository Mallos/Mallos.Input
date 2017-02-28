namespace OpenInput.Test
{
    static class DeviceSets
    {
        // OpenTK input requires a window created somewhere to work.
        public static DeviceSet CreateOpenTK(OpenTK.GameWindow window = null)
        {
            return new DeviceSet(
                "OpenTK",
                new OpenInput.OpenTKKeyboard((window != null) ? window.Keyboard : null),
                new OpenInput.OpenTKMouse((window != null) ? window.Mouse : null),
                (int index) => { return new OpenInput.OpenTKGamePad(index); }
            );
        }

#if WITH_RAWINPUT
        public static InputContext CreateRawInput(IntPtr handle)
        {
            return new InputContext()
            {
                Mouse = new OpenInput.RawInput.Mouse(handle),
                Keyboard = new OpenInput.RawInput.Keyboard(handle),
            };
        }
#endif
    }
}
