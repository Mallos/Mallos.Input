namespace OpenInput.Mechanics
{
    using OpenInput.Trackers;
    using System;
    using System.Collections.Generic;

    public struct InputAction
    {
        public readonly string Name;
        public readonly InputKey Key;

        /// <summary>
        /// Initialize a new <see cref="InputAction"/> structure.
        /// </summary>
        public InputAction(string name, InputKey key)
        {
            if (string.IsNullOrWhiteSpace(name)) throw new ArgumentNullException(nameof(name));

            this.Name = name;
            this.Key = key;
        }
    }

    public struct InputAxis
    {
        public readonly string Name;
        public readonly InputKey Key;
        public readonly float Value;

        /// <summary>
        /// Initialize a new <see cref="InputAxis"/> structure.
        /// </summary>
        public InputAxis(string name, InputKey key, float value)
        {
            if (string.IsNullOrWhiteSpace(name)) throw new ArgumentNullException(nameof(name));
            if (float.IsInfinity(value) || float.IsNaN(value)) throw new ArgumentOutOfRangeException(nameof(value));

            this.Name = name;
            this.Key = key;
            this.Value = value;
        }
    }

    public class InputSystem : ITracker
    {
        public readonly IKeyboard Keyboard;
        public readonly IMouse Mouse;

        public readonly List<InputAction> Actions = new List<InputAction>();
        public readonly List<InputAxis> Axis = new List<InputAxis>();

        private readonly Dictionary<string, bool> inputActions = new Dictionary<string, bool>();
        private readonly Dictionary<string, float> inputAxis = new Dictionary<string, float>();

        public InputSystem(IKeyboard keyboard, IMouse mouse)
        {
            this.Keyboard = keyboard ?? throw new ArgumentNullException(nameof(keyboard));
            this.Mouse = mouse ?? throw new ArgumentNullException(nameof(mouse));
        }

        public IEnumerable<KeyValuePair<string, bool>> GetActionsValues()
        {
            return inputActions;
        }

        public IEnumerable<KeyValuePair<string, float>> GetAxisValues()
        {
            return inputAxis;
        }

        public void Update(float elapsedTime)
        {
            inputActions.Clear();
            inputAxis.Clear();

            var keyboardState = Keyboard.GetCurrentState();
            //var mouseState = Mouse.GetCurrentState();

            foreach (var action in Actions)
            {
                switch (action.Key.Type)
                {
                    case InputKeyType.Keyboard:
                        if (keyboardState.IsKeyDown(action.Key.Key))
                        {
                            if (inputActions.ContainsKey(action.Name))
                            {
                                inputActions[action.Name] = true;
                            }
                            else
                            {
                                inputActions.Add(action.Name, true);
                            }
                        }
                        break;

                    case InputKeyType.Mouse:
                        break;

                    case InputKeyType.GamePad:
                        break;
                }
            }

            foreach (var axis in Axis)
            {
                switch (axis.Key.Type)
                {
                    case InputKeyType.Keyboard:
                        if (keyboardState.IsKeyDown(axis.Key.Key))
                        {
                            if (inputAxis.ContainsKey(axis.Name))
                            {
                                inputAxis[axis.Name] += axis.Value;
                            }
                            else
                            {
                                inputAxis.Add(axis.Name, axis.Value);
                            }
                        }
                        break;

                    case InputKeyType.Mouse:
                        break;

                    case InputKeyType.GamePad:
                        break;
                }
            }
        }

        public bool GetAction(string name)
        {
            return inputActions.ContainsKey(name) ? inputActions[name] : false;
        }

        public float GetAxis(string name)
        {
            return inputAxis.ContainsKey(name) ? inputAxis[name] : 0.0f;
        }

        /// <summary>
        /// Clear the input system of all settings.
        /// </summary>
        public virtual void Clear()
        {
            this.Actions.Clear();
            this.Axis.Clear();
        }
    }
}
