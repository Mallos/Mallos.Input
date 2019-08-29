namespace OpenInput.Mechanics
{
    using System;
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

        /// <summary>
        /// Returns the input type as string.
        /// </summary>
        public string TypeAsString() => Enum.GetName(typeof(InputType), this.Type);

        /// <summary>
        /// Returns the button of this type as string.
        /// </summary>
        public string ButtonAsString()
        {
            switch (this.Type)
            {
                default: return string.Empty;
                case InputType.Keyboard: return this.Key.ToString();
                case InputType.Mouse: return this.MouseButton.ToString();
                case InputType.GamePad: return this.Button.ToString();
            }
        }
        /// <summary>
        /// Returns the button of this type as integer.
        /// </summary>
        public int ButtonAsInteger()
        {
            switch (this.Type)
            {
                default: return 0;
                case InputType.Keyboard: return (int)this.Key;
                case InputType.Mouse: return (int)this.MouseButton;
                case InputType.GamePad: return (int)this.Button;
            }
        }

        /// <inheritdoc />
        public override string ToString() => $"[{this.TypeAsString()}] {this.ButtonAsString()}";
    }
}
