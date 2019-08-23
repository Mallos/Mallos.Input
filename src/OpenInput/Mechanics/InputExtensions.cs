namespace OpenInput.Mechanics
{
    public static class InputExtensions
    {
        public static bool IsKeyDown(this KeyboardState state, InputKey key)
        {
            return key.Type == InputKeyType.Keyboard &&
                   state.IsKeyDown(key.Key);
        }

        public static bool IsButtonDown(this GamePadState state, InputKey key)
        {
            return key.Type == InputKeyType.GamePad &&
                   state.Buttons.IsButtonDown(key.Button);
        }
    }
}
