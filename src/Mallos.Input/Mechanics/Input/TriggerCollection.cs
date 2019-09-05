namespace Mallos.Input.Mechanics.Input
{
    using System.Linq;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Collections.Specialized;

    public abstract class TriggerCollection<TType, TValue> : ObservableCollection<TType>
        where TType : IInputTrigger
    {
        private readonly Dictionary<string, TValue> values = new Dictionary<string, TValue>();
        private readonly Dictionary<string, TValue> tmpValues = new Dictionary<string, TValue>();

        /// <summary>
        /// Gets all the keys that are used.
        /// </summary>
        public List<string> Keys { get; private set; } = new List<string>();

        /// <summary>
        /// Gets the value of a specific trigger.
        /// </summary>
        /// <return>The value</return>
        public TValue GetValue(string name)
        {
            return this.values.ContainsKey(name) ? this.values[name] : default(TValue);
        }

        /// <summary>
        /// Returns all the trigger keys and values.
        /// </summary>
        public IReadOnlyDictionary<string, TValue> GetValues()
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

        protected abstract TValue OnTriggerDown(TType trigger, TValue currentValue);

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
                foreach (var trigger in this.Items)
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

            if (device is IMouse mouse)
            {
                var mouseState = mouse.GetCurrentState();
                foreach (var trigger in this.Items)
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

            if (device is IGamePad gamepad)
            {
                var gamepadState = gamepad.GetCurrentState();
                foreach (var trigger in this.Items)
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
                var touchState = touch.GetCurrentState();
                // TODO: Support touch
            }
        }

        private void OnClicked(TType trigger)
        {
            this.values.TryGetValue(trigger.Name, out var currentValue);
            var newValue = this.OnTriggerDown(trigger, currentValue);
            this.values[trigger.Name] = newValue;
        }
    }
}
