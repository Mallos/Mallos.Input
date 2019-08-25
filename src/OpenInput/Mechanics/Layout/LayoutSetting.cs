namespace OpenInput.Mechanics.Layout
{
    using System.Reflection;

    public struct LayoutSetting
    {
        private readonly Layout layout;
        private readonly PropertyInfo property;

        internal LayoutSetting(
            Layout layout,
            PropertyInfo property,
            string name,
            string description,
            InputKeys keys)
        {
            this.layout = layout;
            this.property = property;
            this.Name = name;
            this.Description = description;
            this.Keys = keys;

            // TriggerAttribute should always exsist on the property.
            var triggerAttr = (TriggerAttribute)property.GetCustomAttribute(typeof(TriggerAttribute), true);
            this.IsReadOnly = triggerAttr.Locked;
        }

        /// <summary>
        /// Gets the name of the setting.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Gets the description of the setting.
        /// </summary>
        public string Description { get; }

        /// <summary>
        /// Gets the keys of the setting.
        /// </summary>
        public InputKeys Keys { get; private set; }

        /// <summary>
        /// Gets wether this property is read only.
        /// </summary>
        public bool IsReadOnly { get; }

        /// <summary>
        /// Set the keys on this setting.
        /// </summary>
        public bool Set(InputKeys keys)
        {
            if (this.IsReadOnly)
            {
                return false;
            }

            // FIXME: I would like to have some logic here to check wether or not this key is already used.
            this.property.SetValue(this.layout, keys);
            this.Keys = keys;

            return true;
        }
    }
}
