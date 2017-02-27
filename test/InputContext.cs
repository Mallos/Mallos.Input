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

        // OpenTK requires a window to exists.
        public static InputContext CreateOpenTK()
        {
            return new InputContext(
                "OpenTK",
                new OpenInput.OpenTK.Keyboard(),
                new OpenInput.OpenTK.Mouse()
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
