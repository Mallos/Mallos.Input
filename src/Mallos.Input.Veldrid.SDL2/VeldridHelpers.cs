namespace Mallos.Input
{
    using Veldrid;

    internal static class VeldridHelpers
    {
        public static MouseButtons ConvertMouseButtons(this MouseButton button)
        {
            switch (button)
            {
                case MouseButton.Left:
                    return MouseButtons.Left;
                case MouseButton.Middle:
                    return MouseButtons.Middle;
                case MouseButton.Right:
                    return MouseButtons.Right;
                case MouseButton.Button1:
                    return MouseButtons.XButton1;
                case MouseButton.Button2:
                    return MouseButtons.XButton2;

                case MouseButton.Button3:
                case MouseButton.Button4:
                case MouseButton.Button5:
                case MouseButton.Button6:
                case MouseButton.Button7:
                case MouseButton.Button8:
                case MouseButton.Button9:
                case MouseButton.LastButton:
                default:
                    return MouseButtons.Empty;
            }
        }

        public static Keys ConvertKey(this Key key)
        {
            switch (key)
            {
                default:
                case Key.Unknown:
                    return Keys.Unknown;
                case Key.ShiftLeft:
                    return Keys.LeftShift;
                case Key.ShiftRight:
                    return Keys.RightShift;
                case Key.ControlLeft:
                    return Keys.LeftControl;
                case Key.ControlRight:
                    return Keys.RightControl;
                case Key.AltLeft:
                    return Keys.LeftAlt;
                case Key.AltRight:
                    return Keys.RightAlt;
                case Key.WinLeft:
                    return Keys.LWin;
                case Key.WinRight:
                    return Keys.RWin;
                case Key.Menu:
                    return Keys.Menu;
                case Key.F1:
                    return Keys.F1;
                case Key.F2:
                    return Keys.F2;
                case Key.F3:
                    return Keys.F3;
                case Key.F4:
                    return Keys.F4;
                case Key.F5:
                    return Keys.F5;
                case Key.F6:
                    return Keys.F6;
                case Key.F7:
                    return Keys.F7;
                case Key.F8:
                    return Keys.F8;
                case Key.F9:
                    return Keys.F9;
                case Key.F10:
                    return Keys.F10;
                case Key.F11:
                    return Keys.F11;
                case Key.F12:
                    return Keys.F12;
                case Key.F13:
                    return Keys.Unknown;
                case Key.F14:
                    return Keys.Unknown;
                case Key.F15:
                    return Keys.Unknown;
                case Key.F16:
                    return Keys.Unknown;
                case Key.F17:
                    return Keys.Unknown;
                case Key.F18:
                    return Keys.Unknown;
                case Key.F19:
                    return Keys.Unknown;
                case Key.F20:
                    return Keys.Unknown;
                case Key.F21:
                    return Keys.Unknown;
                case Key.F22:
                    return Keys.Unknown;
                case Key.F23:
                    return Keys.Unknown;
                case Key.F24:
                    return Keys.Unknown;
                case Key.F25:
                    return Keys.Unknown;
                case Key.F26:
                    return Keys.Unknown;
                case Key.F27:
                    return Keys.Unknown;
                case Key.F28:
                    return Keys.Unknown;
                case Key.F29:
                    return Keys.Unknown;
                case Key.F30:
                    return Keys.Unknown;
                case Key.F31:
                    return Keys.Unknown;
                case Key.F32:
                    return Keys.Unknown;
                case Key.F33:
                    return Keys.Unknown;
                case Key.F34:
                    return Keys.Unknown;
                case Key.F35:
                    return Keys.Unknown;
                case Key.Up:
                    return Keys.Up;
                case Key.Down:
                    return Keys.Down;
                case Key.Left:
                    return Keys.Left;
                case Key.Right:
                    return Keys.Right;
                case Key.Enter:
                    return Keys.Enter;
                case Key.Escape:
                    return Keys.Escape;
                case Key.Space:
                    return Keys.Space;
                case Key.Tab:
                    return Keys.Tab;
                case Key.BackSpace:
                    return Keys.BackSpace;
                case Key.Insert:
                    return Keys.Insert;
                case Key.Delete:
                    return Keys.Delete;
                case Key.PageUp:
                    return Keys.PageUp;
                case Key.PageDown:
                    return Keys.PageDown;
                case Key.Home:
                    return Keys.Home;
                case Key.End:
                    return Keys.End;
                case Key.CapsLock:
                    return Keys.CapsLock;
                case Key.ScrollLock:
                    return Keys.ScrollLock;
                case Key.PrintScreen:
                    return Keys.PrintScreen;
                case Key.Pause:
                    return Keys.Pause;
                case Key.NumLock:
                    return Keys.NumLock;
                case Key.Clear:
                    return Keys.OemClear;
                case Key.Sleep:
                    return Keys.Sleep;
                case Key.Keypad0:
                    return Keys.NumPad0;
                case Key.Keypad1:
                    return Keys.NumPad1;
                case Key.Keypad2:
                    return Keys.NumPad2;
                case Key.Keypad3:
                    return Keys.NumPad3;
                case Key.Keypad4:
                    return Keys.NumPad4;
                case Key.Keypad5:
                    return Keys.NumPad5;
                case Key.Keypad6:
                    return Keys.NumPad6;
                case Key.Keypad7:
                    return Keys.NumPad7;
                case Key.Keypad8:
                    return Keys.NumPad8;
                case Key.Keypad9:
                    return Keys.NumPad9;
                case Key.KeypadDivide:
                    return Keys.Divide;
                case Key.KeypadMultiply:
                    return Keys.Multiply;
                case Key.KeypadSubtract:
                    return Keys.Subtract;
                case Key.KeypadAdd:
                    return Keys.Add;
                case Key.KeypadDecimal:
                    return Keys.Decimal;
                case Key.KeypadEnter:
                    return Keys.Enter;
                case Key.A:
                    return Keys.A;
                case Key.B:
                    return Keys.B;
                case Key.C:
                    return Keys.C;
                case Key.D:
                    return Keys.D;
                case Key.E:
                    return Keys.E;
                case Key.F:
                    return Keys.F;
                case Key.G:
                    return Keys.G;
                case Key.H:
                    return Keys.H;
                case Key.I:
                    return Keys.I;
                case Key.J:
                    return Keys.J;
                case Key.K:
                    return Keys.K;
                case Key.L:
                    return Keys.L;
                case Key.M:
                    return Keys.M;
                case Key.N:
                    return Keys.N;
                case Key.O:
                    return Keys.O;
                case Key.P:
                    return Keys.P;
                case Key.Q:
                    return Keys.Q;
                case Key.R:
                    return Keys.R;
                case Key.S:
                    return Keys.S;
                case Key.T:
                    return Keys.T;
                case Key.U:
                    return Keys.U;
                case Key.W:
                    return Keys.W;
                case Key.X:
                    return Keys.X;
                case Key.Y:
                    return Keys.Y;
                case Key.Z:
                    return Keys.Z;
                case Key.Number0:
                    return Keys.D0;
                case Key.Number1:
                    return Keys.D1;
                case Key.Number2:
                    return Keys.D2;
                case Key.Number3:
                    return Keys.D3;
                case Key.Number4:
                    return Keys.D4;
                case Key.Number5:
                    return Keys.D5;
                case Key.Number6:
                    return Keys.D6;
                case Key.Number7:
                    return Keys.D7;
                case Key.Number8:
                    return Keys.D8;
                case Key.Number9:
                    return Keys.D9;
                case Key.Tilde:
                    return Keys.Unknown;
                case Key.Minus:
                    return Keys.OemMinus;
                case Key.Plus:
                    return Keys.OemPlus;
                case Key.BracketLeft:
                    return Keys.OemOpenBrackets;
                case Key.BracketRight:
                    return Keys.OemCloseBrackets;
                case Key.Semicolon:
                    return Keys.OemSemicolon;
                case Key.Quote:
                    return Keys.OemQuotes;
                case Key.Comma:
                    return Keys.OemComma;
                case Key.Period:
                    return Keys.OemPeriod;
                case Key.Slash:
                    return Keys.Unknown;
                case Key.BackSlash:
                    return Keys.OemBackslash;
                case Key.NonUSBackSlash:
                    return Keys.OemBackslash;
                case Key.LastKey:
                    return Keys.Unknown;
            }
        }
    }
}
