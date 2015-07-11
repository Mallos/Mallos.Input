namespace OpenInput
{
    partial class Keyboard : Device
    {
        public SharpDX.DirectInput.Keyboard PlatformKeyboard { get; internal set; }

        public KeyboardState PlatformGetCurrentState()
        {
            if (PlatformKeyboard.IsDisposed)
                return new KeyboardState();

            PlatformKeyboard.Poll();

            var state = PlatformKeyboard.GetCurrentState();

            Keys[] keys = new Keys[state.PressedKeys.Count];
            for (int i = 0; i < state.PressedKeys.Count; i++)
                keys[i] = KeyboardUtilities.Convert(state.PressedKeys[i]);

            return new KeyboardState(keys);
        }
    }
}
