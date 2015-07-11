namespace OpenInput
{
    using System;

    public class MouseEventArgs : EventArgs
    {
        public MouseState State { get; internal set; }

        public MouseEventArgs(MouseState state)
        {
            this.State = state;
        }
    }

    public class KeyEventArgs : EventArgs
    {
        public Keys Key { get; internal set; }
        public KeyboardState State { get; internal set; }

        public KeyEventArgs(KeyboardState state, Keys key)
        {
            this.Key = key;
            this.State = state;
        }
    }

    public class KeyPressEventArgs : KeyEventArgs
    {
        public char KeyChar { get; internal set; }
        
        public KeyPressEventArgs(KeyboardState state, Keys key, char keyChar)
            : base(state, key)
        {
            this.KeyChar = keyChar;
        }
    }
}