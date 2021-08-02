namespace Mallos.Input.Mechanics.Input
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Collections.Specialized;
    using System.Linq;

    public abstract class TriggerCollection<TType, TValue> : ObservableCollection<TType>
        where TType : IInputTrigger
    {
        private readonly Dictionary<string, TValue> values = new Dictionary<string, TValue>();
        private readonly Dictionary<string, TValue> tmpValues = new Dictionary<string, TValue>();

        /// <summary>
        /// Gets all the keys that are used.
        /// </summary>
        public List<string> Keys { get; private set; } = new List<string>();

        public void Add(params TType[] values)
        {
            if (values != null)
            {
                foreach (TType item in values)
                {
                    base.Add(item);
                }
            }
        }

        /// <summary>
        /// Gets the value of a specific trigger.
        /// </summary>
        /// <return>The value</return>
        public TValue GetValue(string name)
            => this.values.ContainsKey(name) ? this.values[name] : default;

        /// <summary>
        /// Returns all the trigger keys and values.
        /// </summary>
        public IReadOnlyDictionary<string, TValue> GetValues()
        {
            this.tmpValues.Clear();

            foreach (string key in this.Keys)
            {
                this.tmpValues[key] = this.GetValue(key);
            }

            return this.tmpValues;
        }

        /// <summary>
        /// Clear all the current values.
        /// </summary>
        public void ClearValues()
        {
            this.values.Clear();
        }

        protected virtual void BeforeProcessDevices(IDevice[] devices, IReadOnlyDictionary<string, TValue> lastState)
        {
        }

        protected abstract TValue OnTriggerDown(TType trigger, TValue currentValue);

        protected override void OnCollectionChanged(NotifyCollectionChangedEventArgs args)
        {
            this.Keys = new List<string>(this.Items.Select(e => e.Name));
            base.OnCollectionChanged(args);
        }

        internal void Update(params IDevice[] devices)
        {
            this.BeforeProcessDevices(devices, this.values);
            this.values.Clear();

            foreach (IDevice device in devices)
            {
                this.ProcessDevice(device);
            }
        }

        private void ProcessDevice(IDevice device)
        {
            // process all the keyboard events
            if (device is IKeyboard keyboard)
            {
                KeyboardState keyboardState = keyboard.GetCurrentState();
                foreach (TType trigger in this.Items)
                {
                    if (trigger.Key.Type != InputType.Keyboard)
                    {
                        continue;
                    }

                    if (keyboardState.IsKeyDown(trigger.Key.Key))
                    {
                        this.OnClicked(trigger);
                    }
                }
            }

            // process all the mouse events
            if (device is IMouse mouse)
            {
                MouseState mouseState = mouse.GetCurrentState();
                foreach (TType trigger in this.Items)
                {
                    if (trigger.Key.Type != InputType.Mouse)
                    {
                        continue;
                    }

                    if (mouseState.IsButtonDown(trigger.Key.MouseButton))
                    {
                        this.OnClicked(trigger);
                    }
                }
            }

            // process all the gamepad events
            if (device is IGamePad gamepad)
            {
                GamePadState gamepadState = gamepad.GetCurrentState();
                foreach (TType trigger in this.Items)
                {
                    if (trigger.Key.Type != InputType.GamePad)
                    {
                        continue;
                    }

                    if (gamepadState.IsButtonDown(trigger.Key.Button))
                    {
                        this.OnClicked(trigger);
                    }
                }
            }

            if (device is ITouchDevice touch)
            {
                Touch.TouchCollection touchState = touch.GetCurrentState();
                // TODO: Support touch
            }
        }

        private void OnClicked(TType trigger)
        {
            this.values.TryGetValue(trigger.Name, out TValue currentValue);
            TValue newValue = this.OnTriggerDown(trigger, currentValue);
            this.values[trigger.Name] = newValue;
        }
    }
}
