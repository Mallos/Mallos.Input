namespace Mallos.Input.Mechanics.Input
{
    using System.Collections.Generic;
    using System.Linq;

    public class ActionCollection : TriggerCollection<InputAction, bool>
    {
        private Dictionary<string, bool> lastValues = new Dictionary<string, bool>();

        /// <summary>
        /// Gets the value of a specific trigger.
        /// </summary>
        /// <return>The value</return>
        public bool GetNewValue(string name)
        {
            bool active = this.GetValue(name);
            bool lastActive = this.lastValues.ContainsKey(name) ? this.lastValues[name] : false;

            return active && !lastActive;
        }

        /// <summary>
        /// Helpers methods for making it easier to add a new action.
        /// </summary>
        public void Add(string name, InputKey key)
        {
            base.Add(new InputAction(name, key));
        }

        protected override void BeforeProcessDevices(IDevice[] devices, IReadOnlyDictionary<string, bool> lastState)
        {
            // shallow clone the last state
            this.lastValues = lastState.ToDictionary(
                entry => entry.Key,
                entry => entry.Value
            );

            base.BeforeProcessDevices(devices, lastState);
        }

        protected override bool OnTriggerDown(InputAction trigger, bool currentValue)
        {
            return true;
        }
    }
}
