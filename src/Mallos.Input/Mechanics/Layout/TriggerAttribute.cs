namespace Mallos.Input.Mechanics.Layout
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
        public bool IsReadOnly { get; }

        public TriggerAttribute(string name, bool isReadOnly)
        {
            this.Name = name;
            this.IsReadOnly = isReadOnly;
        }
    }
}
