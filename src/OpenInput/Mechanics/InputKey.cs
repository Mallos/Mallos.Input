namespace OpenInput.Mechanics
{
    using System.Runtime.InteropServices;

    public enum InputKeyType : byte
    {
        Keyboard,
        Mouse,
        GamePad,
    }

    [StructLayout(LayoutKind.Explicit)]
    public struct InputKey
    {
        [FieldOffset(0)] public readonly InputKeyType Type;

        [FieldOffset(1)] public readonly Keys Key;
        [FieldOffset(1)] public readonly Buttons Button;

        public InputKey(Keys key)
        {
            this.Type = InputKeyType.Keyboard;
            this.Button = 0;
            this.Key = key;
        }

        public InputKey(Buttons button)
        {
            this.Type = InputKeyType.GamePad;
            this.Key = 0;
            this.Button = button;
        }

        public static implicit operator InputKey(Keys key) => new InputKey(key);
        public static implicit operator InputKey(Buttons button) => new InputKey(button);

        public static bool operator ==(InputKey a, InputKey b)
        {
            if (a.Type != b.Type)
                return false;

            switch (a.Type)
            {
                default: return false;
                case InputKeyType.Keyboard: return a.Key == b.Key;
                case InputKeyType.GamePad: return a.Button == b.Button;
            }
        }

        public static bool operator !=(InputKey a, InputKey b)
        {
            return !(a == b);
        }

        public override string ToString()
        {
            switch (Type)
            {
                case InputKeyType.Keyboard: return $"[Keyboard] { Key }";
                case InputKeyType.GamePad: return $"[GamePad] { Button }";
            }

            return base.ToString();
        }
    }

    public static class InputExtensions
    {
        public static bool IsKeyDown(this KeyboardState state, InputKey key)
        {
            if (key.Type != InputKeyType.Keyboard)
                return false;

            return state.IsKeyDown(key.Key);
        }

        public static bool IsButtonDown(this GamePadState state, InputKey key)
        {
            if (key.Type != InputKeyType.GamePad)
                return false;

            return state.Buttons.IsButtonDown(key.Button);
        }
    }
}
