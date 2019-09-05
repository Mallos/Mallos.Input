namespace OpenInput.Mechanics
{
    using OpenInput.Trackers;
    using OpenInput.Mechanics.Input;
    using System;

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
        public bool GetAction(string name) => this.Actions.GetValue(name);

        /// <summary>
        /// Gets the value of a specific axis.
        /// </summary>
        /// <return>The value</return>
        public float GetAxis(string name) => this.Axis.GetValue(name);

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
