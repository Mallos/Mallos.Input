namespace OpenInput.Mechanics
{
    using System;
    using System.Collections.Generic;
    using OpenInput.Trackers;

    public interface IController
    {
        void AttachInput(InputManagerEntry entry);

        void DeattachInput(InputManagerEntry entry);

        void UpdateController(float elapsedTime, InputSystem input);
    }

    public abstract class InputManagerEntry
    {
        internal InputManagerEntry(
            InputSystem inputSystem,
            ComboTracker comboTracker)
        {
            this.InputSystem = inputSystem;
            this.ComboTracker = comboTracker;
        }

        public InputSystem InputSystem { get; }
        public ComboTracker ComboTracker { get; }
    }

    /// <summary>
    /// A complete manager of handling joining and leaving input devices to
    /// controllers. This removes all the need for handling all odd cases
    /// when allowing a new player to leave and exit the game.
    /// </summary>
    public abstract class InputManager<TController> : ITracker
        where TController : IController
    {
        static readonly int KeyboardMouseKey = 1010101010;

        private readonly Dictionary<int, Entry> entries = new Dictionary<int, Entry>();

        public InputManager(DeviceSet deviceSet)
        {
            this.DeviceSet = deviceSet;
        }

        /// <summary>
        /// Gets the associated device set.
        /// </summary>
        public DeviceSet DeviceSet { get; }

        /// <summary>
        /// Gets or sets wether or not we can register new controllers now.
        /// </summary>
        public bool AllowRegister { get; set; } = false;

        /// <summary>
        /// Gets or sets the maximum amount of connected controllers.
        /// </summary>
        public int MaxControllers { get; set; } = 4; // FIXME: Disconnect if changed and limit it

        /// <summary>
        /// Gets all the connected controllers.
        /// </summary>
        public Dictionary<int, Entry>.ValueCollection Controllers => this.entries.Values;

        /// <inheritdoc />
        public void Update(float elapsedTime)
        {
            if (this.AllowRegister && this.MaxControllers <= this.entries.Count)
            {
                this.CheckAllowRegister();
            }

            if (this.entries.Count > 0)
            {
                foreach (var gamepad in this.DeviceSet.GamePads)
                {
                    var gamepadHashCode = gamepad.GetHashCode();
                    if (this.entries.ContainsKey(gamepadHashCode))
                    {
                        var gamepadState = gamepad.GetCurrentState();
                    }
                }

                if (this.entries.ContainsKey(KeyboardMouseKey))
                {
                    var keyboardState = this.DeviceSet.Keyboard.GetCurrentState();

                }
            }
        }

        protected virtual bool CanJoin(InputKey key, IDevice device)
        {
            return false;
        }

        protected virtual TController CreateController(InputKey key, IDevice device)
        {
            return default(TController);
        }

        protected virtual void OnJoin(Entry entry)
        {
        }

        protected virtual void OnLeave(Entry entry)
        {
        }

        private void CheckAllowRegister()
        {
            var buttonTypes = (Buttons[])Enum.GetValues(typeof(Buttons));
            foreach (var gamepad in this.DeviceSet.GamePads)
            {
                var gamepadHashCode = gamepad.GetHashCode();
                if (this.entries.ContainsKey(gamepadHashCode))
                {
                    continue;
                }

                var gamepadState = gamepad.GetCurrentState();
                foreach (var button in buttonTypes)
                {
                    var inputKey = new InputKey(button);
                    if (gamepadState.IsButtonDown(button) &&
                        this.CanJoin(inputKey, gamepad))
                    {
                        this.RegisterDevice(gamepadHashCode, inputKey, gamepad);
                    }
                }
            }

            if (!this.entries.ContainsKey(KeyboardMouseKey))
            {
                var keyboardState = this.DeviceSet.Keyboard.GetCurrentState();
                foreach (var key in keyboardState.Keys)
                {
                    var inputKey = new InputKey(key);
                    if (CanJoin(inputKey, this.DeviceSet.Keyboard))
                    {
                        this.RegisterDevice(KeyboardMouseKey, inputKey, this.DeviceSet.Keyboard);
                    }
                }
            }
        }

        private void RegisterDevice(int deviceHashCode, InputKey inputKey, IDevice device)
        {
            if (this.MaxControllers > this.entries.Count)
            {
                return;
            }

            var controller = CreateController(inputKey, device); // FIXME:
            if (controller == null)
            {
                return;
            }

            var inputSystem = new InputSystem(null, null);
            var comboTracker = new ComboTracker(null);

            this.entries[deviceHashCode] = new Entry(controller, inputSystem, comboTracker);

            this.OnJoin(this.entries[deviceHashCode]);
        }

        public sealed class Entry : InputManagerEntry
        {
            internal Entry(
                TController controller,
                InputSystem inputSystem,
                ComboTracker comboTracker)
                : base(inputSystem, comboTracker)
            {
                this.Controller = controller;
            }

            public TController Controller { get; }
        }
    }
}
