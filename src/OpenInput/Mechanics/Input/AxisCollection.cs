namespace OpenInput.Mechanics.Input
{
    using System.Linq;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Collections.Specialized;

    public class AxisCollection : ObservableCollection<InputAxis>
    {
        private readonly Dictionary<string, float> values = new Dictionary<string, float>();
        private readonly Dictionary<string, float> tmpValues = new Dictionary<string, float>();

        public List<string> Keys { get; private set; } = new List<string>();

        /// <summary>
        /// Helpers methods for making it easier to add a new action.
        /// </summary>
        public void Add(string name, InputKey key, float value)
        {
            base.Add(new InputAxis(name, key, value));
        }

        /// <summary>
        /// Gets the value of a specific axis.
        /// </summary>
        /// <return>The value</return>
        public float GetValue(string name)
        {
            return this.values.ContainsKey(name) ? this.values[name] : 0.0f;
        }

        /// <summary>
        /// Returns all the axis keys and values.
        /// </summary>
        public IReadOnlyDictionary<string, float> GetValues()
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
                foreach (var axis in this.Items)
                {
                    if (axis.Key.Type != InputKeyType.Keyboard)
                    {
                        continue;
                    }

                    if (keyboardState.IsKeyDown(axis.Key.Key))
                    {
                        if (this.values.ContainsKey(axis.Name))
                        {
                            this.values[axis.Name] += axis.Value;
                        }
                        else
                        {
                            this.values.Add(axis.Name, axis.Value);
                        }
                    }
                }
            }

            if (device is IMouse mouse)
            {
                var mouseState = mouse.GetCurrentState();
                foreach (var axis in this.Items)
                {
                    if (axis.Key.Type != InputKeyType.Mouse)
                    {
                        continue;
                    }

                    if (mouseState.IsButtonDown(axis.Key.MouseButton))
                    {
                        if (this.values.ContainsKey(axis.Name))
                        {
                            this.values[axis.Name] += axis.Value;
                        }
                        else
                        {
                            this.values.Add(axis.Name, axis.Value);
                        }
                    }
                }
            }

            if (device is IGamePad gamepad)
            {
                var gamepadState = gamepad.GetCurrentState();
                foreach (var axis in this.Items)
                {
                    if (axis.Key.Type != InputKeyType.GamePad)
                    {
                        continue;
                    }

                    if (gamepadState.IsButtonDown(axis.Key.Button))
                    {
                        if (this.values.ContainsKey(axis.Name))
                        {
                            this.values[axis.Name] += axis.Value;
                        }
                        else
                        {
                            this.values.Add(axis.Name, axis.Value);
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
