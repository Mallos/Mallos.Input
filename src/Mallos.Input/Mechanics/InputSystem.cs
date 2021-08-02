namespace Mallos.Input.Mechanics
{
    using Mallos.Input.Trackers;
    using Mallos.Input.Mechanics.Input;
    using System;

    /// <summary>
    /// <see cref="InputSystem"/> have 2 types of input modes, Action and Axis modes,
    /// which can have "friendly names".
    ///
    /// Actions are designed to only handle pressed and released states,
    /// for example a key press, mouse button, or a gamepad button.
    /// So an action is either True or False at all times.
    ///
    /// Axes are designed to handle multiple input types that will
    /// create a vector which is useful for handling movement input.
    /// For example with a gamepad you can handle transitional movement.
    /// </summary>
    public class InputSystem : ITracker
    {
        /// <summary>
        /// Initializes a new instance of <see cref="InputSystem"/>.
        /// </summary>
        public InputSystem(params IDevice[] devices)
        {
            if (devices == null || devices.Length == 0)
            {
                throw new ArgumentNullException(nameof(devices));
            }

            this.Devices = devices;
        }

        /// <summary>
        /// Gets all the devices used.
        /// </summary>
        public IDevice[] Devices { get; }

        /// <summary>
        /// Gets the action collection.
        /// </summary>
        public ActionCollection Actions { get; } = new ActionCollection();

        /// <summary>
        /// Gets the axis collection.
        /// </summary>
        public AxisCollection Axis { get; } = new AxisCollection();

        /// <inheritdoc />
        public void Update(float elapsedTime)
        {
            this.Actions.Update(this.Devices);
            this.Axis.Update(this.Devices);
        }

        /// <summary>
        /// Gets the value of a specific action.
        /// </summary>
        /// <return>The value</return>
        public bool GetAction(string name)
            => this.Actions.GetValue(name);

        /// <summary>
        /// Gets wether the specific action just changed to true.
        /// </summary>
        /// <return>The value</return>
        public bool GetNewAction(string name)
            => this.Actions.GetNewValue(name);

        /// <summary>
        /// Gets the value of a specific axis.
        /// </summary>
        /// <return>The value</return>
        public float GetAxis(string name)
            => this.Axis.GetValue(name);

        /// <summary>
        /// Clear the input system of all settings.
        /// </summary>
        public void Clear()
        {
            this.Actions.Clear();
            this.Axis.Clear();
        }

        /// <summary>
        /// Clear all the current values.
        /// </summary>
        public void ClearValues()
        {
            this.Actions.ClearValues();
            this.Axis.ClearValues();
        }
    }
}
