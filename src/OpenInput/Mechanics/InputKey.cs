namespace OpenInput.Mechanics
{
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Explicit)]
    public struct InputKey
    {
        [FieldOffset(0)] public readonly InputType Type;

        [FieldOffset(1)] public readonly Keys Key;
        [FieldOffset(1)] public readonly Buttons Button;
        [FieldOffset(1)] public readonly MouseButtons MouseButton;

        public InputKey(Keys key)
        {
            this.Type = InputType.Keyboard;
            this.Button = 0;
            this.MouseButton = 0;
            this.Key = key;
        }

        public InputKey(Buttons button)
        {
            this.Type = InputType.GamePad;
            this.Key = 0;
            this.MouseButton = 0;
            this.Button = button;
        }

        public InputKey(MouseButtons button)
        {
            this.Type = InputType.Mouse;
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
                case InputType.Keyboard: return a.Key == b.Key;
                case InputType.GamePad: return a.Button == b.Button;
                case InputType.Mouse: return a.MouseButton == b.MouseButton;
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
                case InputType.Keyboard: return $"[Keyboard] {this.Key}";
                case InputType.Mouse: return $"[Mouse] {this.MouseButton}";
                case InputType.GamePad: return $"[GamePad] {this.Button}";
            }

            return base.ToString();
        }
    }
}
