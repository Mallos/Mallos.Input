namespace OpenInput.Windows
{
    using System;
    using DI_Keyboard = SharpDX.DirectInput.Keyboard;

    public class Keyboard : IKeyboard
    {
        public string Name => "Keyboard";

        internal readonly DI_Keyboard keyboard;

        internal Keyboard(DI_Keyboard keyboard)
        {
            this.keyboard = keyboard;
            this.keyboard.Acquire();
        }

        public KeyboardState GetCurrentState()
        {
            if (keyboard.IsDisposed)
                return new KeyboardState();

            keyboard.Poll();

            var state = keyboard.GetCurrentState();

            Keys[] keys = new Keys[state.PressedKeys.Count];
            for (int i = 0; i < state.PressedKeys.Count; i++)
                keys[i] = SharpDXConverters.Convert(state.PressedKeys[i]);

            return new KeyboardState(keys);
        }
    }
}
