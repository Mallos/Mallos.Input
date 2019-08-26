namespace OpenInput.Mechanics.Input
{
    using System.Linq;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Collections.Specialized;

    public class ActionCollection : ObservableCollection<InputAction>
    {
        private readonly Dictionary<string, bool> values = new Dictionary<string, bool>();
        private readonly Dictionary<string, bool> tmpValues = new Dictionary<string, bool>();

        public List<string> Keys { get; private set; } = new List<string>();

        /// <summary>
        /// Helpers methods for making it easier to add a new action.
        /// </summary>
        public void Add(string name, InputKey key)
        {
            base.Add(new InputAction(name, key));
        }

        /// <summary>
        /// Gets the value of a specific action.
        /// </summary>
        /// <return>The value</return>
        public bool GetValue(string name)
        {
            return this.values.ContainsKey(name) ? this.values[name] : false;
        }

        /// <summary>
        /// Returns all the action keys and values.
        /// </summary>
        public IReadOnlyDictionary<string, bool> GetValues()
        {
            this.tmpValues.Clear();

            foreach (var key in this.Keys)
            {
                this.tmpValues[key] = this.GetValue(key);
            }

            return tmpValues;
        }

        /// <summary>
        /// Clear all the current values.
        /// </summary>
        public void ClearValues()
        {
            this.values.Clear();
        }

        protected override void OnCollectionChanged(NotifyCollectionChangedEventArgs args)
        {
            this.Keys = new List<string>(this.Items.Select(e => e.Name));
            base.OnCollectionChanged(args);
        }

        internal void Update(params IDevice[] devices)
        {
            this.values.Clear();

            foreach (var device in devices)
            {
                this.ProcessDevice(device);
            }
        }

        private void ProcessDevice(IDevice device)
        {
            if (device is IKeyboard keyboard)
            {
                var keyboardState = keyboard.GetCurrentState();
                foreach (var action in this.Items)
                {
                    if (action.Key.Type != InputKeyType.Keyboard)
                    {
                        continue;
                    }

                    if (keyboardState.IsKeyDown(action.Key.Key))
                    {
                        if (this.values.ContainsKey(action.Name))
                        {
                            this.values[action.Name] = true;
                        }
                        else
                        {
                            this.values.Add(action.Name, true);
                        }
                    }
                }
            }

            if (device is IMouse mouse)
            {
                var mouseState = mouse.GetCurrentState();
                foreach (var action in this.Items)
                {
                    if (action.Key.Type != InputKeyType.Mouse)
                    {
                        continue;
                    }

                    if (mouseState.IsButtonDown(action.Key.MouseButton))
                    {
                        if (this.values.ContainsKey(action.Name))
                        {
                            this.values[action.Name] = true;
                        }
                        else
                        {
                            this.values.Add(action.Name, true);
                        }
                    }
                }
            }

            if (device is IGamePad gamepad)
            {
                var gamepadState = gamepad.GetCurrentState();
                foreach (var action in this.Items)
                {
                    if (action.Key.Type != InputKeyType.GamePad)
                    {
                        continue;
                    }

                    if (gamepadState.IsButtonDown(action.Key.Button))
                    {
                        if (this.values.ContainsKey(action.Name))
                        {
                            this.values[action.Name] = true;
                        }
                        else
                        {
                            this.values.Add(action.Name, true);
                        }
                    }
                }
            }

            if (device is ITouchDevice touch)
            {
                var touchState = touch.GetCurrentState();
                // TODO: Support touch
            }
        }
    }
}
