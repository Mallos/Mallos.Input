namespace OpenInput.Test
{
    class InputContext
    {
        public readonly string Name;
        public readonly IKeyboard Keyboard;
        public readonly IMouse Mouse;
        
        public InputContext(string name,
            IKeyboard keyboard,
            IMouse mouse)
        {
            this.Name = name;
            this.Keyboard = keyboard;
            this.Mouse = mouse;

            if (this.Keyboard != null)
            {

            }
        }

        public static InputContext CreateEmpty()
        {
            return new InputContext(
                "Empty",
                new OpenInput.Dummy.DummyKeyboard(),
                new OpenInput.Dummy.DummyMouse()
            );
        }

        // OpenTK input requires a window created somewhere to work.
        public static InputContext CreateOpenTK(OpenTK.GameWindow window = null)
        {
            return new InputContext(
                "OpenTK",
                new OpenInput.OpenTKKeyboard((window != null) ? window.Keyboard : null),
                new OpenInput.OpenTKMouse((window != null) ? window.Mouse : null)
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
