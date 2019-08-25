namespace OpenInput.Mechanics.Layout
{
    using System;

    public abstract class TriggerAttribute : Attribute
    {
        /// <summary>
        /// Gets the trigger name.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Gets wether of not this trigger is readonly and not allowed to be rebinded.
        /// </summary>
        public bool Locked { get; }

        public TriggerAttribute(string name, bool locked)
        {
            this.Name = name;
            this.Locked = locked;
        }
    }

    public class ActionTriggerAttribute : TriggerAttribute
    {
        public ActionTriggerAttribute(string name, bool locked = false)
            : base(name, locked)
        {
        }
    }

    public class AxisTriggerAttribute : TriggerAttribute
    {
        public float Value { get; }

        public AxisTriggerAttribute(
            string name,
            float value,
            bool locked = false)
            : base(name, locked)
        {
            this.Value = value;
        }
    }
}
