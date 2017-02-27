namespace OpenInput
{
    using OpenTK.Input;

    static class OpenTKHelpers
    {
        public static Keys KeyToKeys(this Key key)
        {
            switch (key)
            {
                default: return Keys.Unknown;

                /* Keys we are not using */
                case Key.F13: case Key.F14: case Key.F15:
                case Key.F16: case Key.F17: case Key.F18:
                case Key.F19: case Key.F20: case Key.F21:
                case Key.F22: case Key.F23: case Key.F24:
                case Key.F25: case Key.F26: case Key.F27:
                case Key.F28: case Key.F29: case Key.F30:
                case Key.F31: case Key.F32: case Key.F33:
                case Key.F34: case Key.F35:
                case Key.WinLeft:
                case Key.WinRight:
                case Key.Unknown: return Keys.Unknown;

                /* Modifiers */
                case Key.ShiftLeft: return Keys.LShiftKey;
                case Key.ShiftRight: return Keys.RShiftKey;
                case Key.ControlLeft: return Keys.LControlKey;
                case Key.ControlRight: return Keys.RControlKey;
                case Key.AltLeft: return Keys.LAltKey;
                case Key.AltRight: return Keys.RAltKey;
                case Key.Menu: return Keys.Menu;

                /* Function keys */
                case Key.F1: case Key.F2: case Key.F3:
                case Key.F4: case Key.F5: case Key.F6:
                case Key.F7: case Key.F8: case Key.F9:
                case Key.F10: case Key.F11: case Key.F12:
                    return (Keys)((int)Keys.F1 + ((int)key - (int)Key.F1));

                /* Direction arrows */
                case Key.Up: return Keys.Up;
                case Key.Down: return Keys.Down;
                case Key.Left: return Keys.Left;
                case Key.Right: return Keys.Right;

                /* */
                case Key.Enter: return Keys.Enter;
                case Key.KeypadEnter: return Keys.Enter;
                case Key.Escape: return Keys.Escape;
                case Key.Space: return Keys.Space;
                case Key.Tab: return Keys.Tab;
                case Key.BackSpace: return Keys.BackSpace;
                case Key.Insert: return Keys.Insert;
                case Key.Delete: return Keys.Delete;
                case Key.PageUp: return Keys.PageUp;
                case Key.PageDown: return Keys.PageDown;
                case Key.Home: return Keys.Home;
                case Key.End: return Keys.End;
                case Key.CapsLock: return Keys.CapsLock;
                case Key.ScrollLock: return Keys.ScrollLock;
                case Key.PrintScreen: return Keys.PrintScreen;
                case Key.Pause: return Keys.Pause;
                case Key.NumLock: return Keys.NumLock;

                /* Keypad keys */
                case Key.Keypad0:
                case Key.Keypad1: case Key.Keypad2: case Key.Keypad3:
                case Key.Keypad4: case Key.Keypad5: case Key.Keypad6:
                case Key.Keypad7: case Key.Keypad8: case Key.Keypad9:
                    return (Keys)((int)Keys.NumPad0 + ((int)key - (int)Key.Keypad0));

                /* Numbers */
                case Key.Number0:
                case Key.Number1: case Key.Number2: case Key.Number3:
                case Key.Number4: case Key.Number5: case Key.Number6:
                case Key.Number7: case Key.Number8: case Key.Number9:
                    return (Keys)((int)Keys.D0 + ((int)key - (int)Key.Number0));

                /* Letters */
                case Key.A: case Key.B:
                case Key.C: case Key.D: case Key.E:
                case Key.F: case Key.G: case Key.H:
                case Key.I: case Key.J: case Key.K:
                case Key.L: case Key.M: case Key.N:
                case Key.O: case Key.P: case Key.Q:
                case Key.R: case Key.S: case Key.T:
                case Key.U: case Key.V: case Key.W:
                case Key.X: case Key.Y: case Key.Z:
                    return (Keys)((int)Keys.A + ((int)key - (int)Key.A));

                /* Symbols */
                case Key.KeypadDivide: return Keys.Divide;
                case Key.KeypadMultiply: return Keys.Multiply;
                case Key.KeypadSubtract: return Keys.Subtract;
                case Key.KeypadAdd: return Keys.Add;
                case Key.KeypadDecimal: return Keys.Decimal;
                case Key.Tilde: return Keys.OemTilde;
                case Key.Minus: return Keys.Minus;
                case Key.Plus: return Keys.Add;
                case Key.Semicolon: return Keys.OemSemicolon;
                case Key.Quote: return Keys.OemQuotes;
                case Key.Comma: return Keys.OemComma;
                case Key.Period: return Keys.OemPeriod;
                case Key.BackSlash: return Keys.OemBackslash;
            }
        }
    }
}
