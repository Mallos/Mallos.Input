namespace OpenInput
{
    using OpenTK.Input;

    static class OpenTKHelpers
    {
        public static Keys KeyToKeys(this Key key)
        {
            switch (key)
            {
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

                case Key.Enter:
                    break;
                case Key.Escape:
                    break;
                case Key.Space:
                    break;
                case Key.Tab:
                    break;
                case Key.BackSpace:
                    break;
                case Key.Insert:
                    break;
                case Key.Delete:
                    break;
                case Key.PageUp:
                    break;
                case Key.PageDown:
                    break;
                case Key.Home:
                    break;
                case Key.End:
                    break;
                case Key.CapsLock:
                    break;
                case Key.ScrollLock:
                    break;
                case Key.PrintScreen:
                    break;
                case Key.Pause:
                    break;
                case Key.NumLock:
                    break;
                case Key.Clear:
                    break;
                case Key.Sleep:
                    break;
                case Key.Keypad0:
                    break;
                case Key.Keypad1:
                    break;
                case Key.Keypad2:
                    break;
                case Key.Keypad3:
                    break;
                case Key.Keypad4:
                    break;
                case Key.Keypad5:
                    break;
                case Key.Keypad6:
                    break;
                case Key.Keypad7:
                    break;
                case Key.Keypad8:
                    break;
                case Key.Keypad9:
                    break;
                case Key.KeypadDivide:
                    break;
                case Key.KeypadMultiply:
                    break;
                case Key.KeypadSubtract:
                    break;
                case Key.KeypadAdd:
                    break;
                case Key.KeypadDecimal:
                    break;
                case Key.KeypadEnter:
                    break;
                case Key.A:
                    break;
                case Key.B:
                    break;
                case Key.C:
                    break;
                case Key.D:
                    break;
                case Key.E:
                    break;
                case Key.F:
                    break;
                case Key.G:
                    break;
                case Key.H:
                    break;
                case Key.I:
                    break;
                case Key.J:
                    break;
                case Key.K:
                    break;
                case Key.L:
                    break;
                case Key.M:
                    break;
                case Key.N:
                    break;
                case Key.O:
                    break;
                case Key.P:
                    break;
                case Key.Q:
                    break;
                case Key.R:
                    break;
                case Key.S:
                    break;
                case Key.T:
                    break;
                case Key.U:
                    break;
                case Key.V:
                    break;
                case Key.W:
                    break;
                case Key.X:
                    break;
                case Key.Y:
                    break;
                case Key.Z:
                    break;
                case Key.Number0:
                    break;
                case Key.Number1:
                    break;
                case Key.Number2:
                    break;
                case Key.Number3:
                    break;
                case Key.Number4:
                    break;
                case Key.Number5:
                    break;
                case Key.Number6:
                    break;
                case Key.Number7:
                    break;
                case Key.Number8:
                    break;
                case Key.Number9:
                    break;
                case Key.Tilde:
                    break;
                case Key.Minus:
                    break;
                case Key.Plus:
                    break;
                case Key.BracketLeft:
                    break;
                case Key.BracketRight:
                    break;
                case Key.Semicolon:
                    break;
                case Key.Quote:
                    break;
                case Key.Comma:
                    break;
                case Key.Period:
                    break;
                case Key.Slash:
                    break;
                case Key.BackSlash:
                    break;
                case Key.NonUSBackSlash:
                    break;
                case Key.LastKey:
                    break;
                default:
                    break;
            }
            return Keys.Unknown;
        }
    }
}
