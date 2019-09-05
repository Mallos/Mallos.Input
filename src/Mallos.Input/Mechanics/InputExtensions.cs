namespace Mallos.Input.Mechanics
{
    public static class InputExtensions
    {
        public static bool IsKeyDown(this KeyboardState state, InputKey key)
        {
            return key.Type == InputType.Keyboard &&
                   state.IsKeyDown(key.Key);
        }

        public static bool IsButtonDown(this GamePadState state, InputKey key)
        {
            return key.Type == InputType.GamePad &&
                   state.Buttons.IsButtonDown(key.Button);
        }
    }
}
