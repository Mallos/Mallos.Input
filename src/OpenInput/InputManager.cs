namespace OpenInput
{
    using System;
    using System.Threading.Tasks;

    public delegate void MouseEventHandler(object sender, MouseEventArgs e);
    public delegate void KeyEventHandler(object sender, KeyEventArgs e);
    public delegate void KeyPressEventHandler(object sender, KeyPressEventArgs e);

    public partial class InputManager
    {
        public static InputManager Current => current ?? (current = new InputManager());
        private static InputManager current;

        public event Action<Device> DeviceConnected;
        public event Action<Device> DeviceDisconnected;

        public event MouseEventHandler MouseMove;
        public event MouseEventHandler MouseClick;
        public event MouseEventHandler MouseDown;
        public event MouseEventHandler MouseUp;
        public event MouseEventHandler MouseWheel;

        public event KeyEventHandler KeyDown;
        public event KeyPressEventHandler KeyPress;
        public event KeyEventHandler KeyUp;

        public Mouse Mouse => currentMouse;
        public Keyboard Keyboard => currentKeyboard;

        // We only support one mouse and one keyboard
        private Mouse currentMouse;
        private Keyboard currentKeyboard;

        private KeyboardState currentKeyboardState, previusKeyboardState;

        public InputManager()
        {
            if (current == null) current = this;

#if !PCL
            this.PlatformInitialize();
#endif
        }

        public async Task Update()
        {
            // TODO: update mouse

            await Task.Run(() =>
            {
                if (currentKeyboard != null)
                {
                    currentKeyboardState = currentKeyboard.GetCurrentState();

                    var shift = currentKeyboardState.IsKeyDown(Keys.LeftShift);
                    var compare = currentKeyboardState.Compare(previusKeyboardState);
                    
                    if (KeyPress != null)
                    {
                        var e = new KeyPressEventArgs(currentKeyboardState, Keys.Unknown, '\0');
                        foreach (var key in compare.Item1)
                        {
                            e.Key = key;
                            
                            if (InputHelper.IsLetter(key))
                            {
                                e.KeyChar = shift ? (char)(key) : (char)(key + 32);
                            }
                            else
                            {
                                e.KeyChar = '\0';
                            }

                            this.KeyPress(this, e);
                        }
                    }

                    if (KeyDown != null)
                    {
                        var e = new KeyEventArgs(currentKeyboardState, Keys.Unknown);
                        foreach (var key in compare.Item1)
                        {
                            e.Key = key;
                            this.KeyDown(this, e);
                        }
                    }

                    if (KeyUp != null)
                    {
                        var e = new KeyEventArgs(currentKeyboardState, Keys.Unknown);
                        foreach (var key in compare.Item2)
                        {
                            e.Key = key;
                            this.KeyUp(this, e);
                        }
                    }

                    previusKeyboardState = currentKeyboardState;
                }
            });
        }
    }
}
