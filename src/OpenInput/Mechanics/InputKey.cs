namespace OpenInput.Mechanics
{
    using System.Runtime.InteropServices;

    public enum InputKeyType : byte
    {
        Keyboard,
        Mouse,
        GamePad,
        Touch,
    }

    [StructLayout(LayoutKind.Explicit)]
    public struct InputKey
    {
        [FieldOffset(0)] public readonly InputKeyType Type;

        [FieldOffset(1)] public readonly Keys Key;
        [FieldOffset(1)] public readonly Buttons Button;
        [FieldOffset(1)] public readonly MouseButtons MouseButton;

        public InputKey(Keys key)
        {
            this.Type = InputKeyType.Keyboard;
            this.Button = 0;
            this.MouseButton = 0;
            this.Key = key;
        }

        public InputKey(Buttons button)
        {
            this.Type = InputKeyType.GamePad;
            this.Key = 0;
            this.MouseButton = 0;
            this.Button = button;
        }

        public InputKey(MouseButtons button)
        {
            this.Type = InputKeyType.Mouse;
            this.Key = 0;
            this.Button = 0;
            this.MouseButton = button;
        }

        public static implicit operator InputKey(Keys key) => new InputKey(key);
        public static implicit operator InputKey(Buttons button) => new InputKey(button);
        public static implicit operator InputKey(MouseButtons button) => new InputKey(button);

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
            switch (this.Type)
            {
                case InputKeyType.Keyboard: return $"[Keyboard] {this.Key}";
                case InputKeyType.Mouse: return $"[Mouse] {this.MouseButton}";
                case InputKeyType.GamePad: return $"[GamePad] {this.Button}";
            }

            return base.ToString();
        }
    }
}
