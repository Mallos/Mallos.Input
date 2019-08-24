namespace OpenInput.Mechanics.Layout
{
    using System.Linq;
    using System.Reflection;

    /// <summary>
    /// A class that makes it easier to handle input layouts for your players.
    /// </summary>
    public abstract class Layout
    {
        public Layout(string layoutId)
        {
            this.LayoutId = layoutId;
        }

        /// <summary>
        /// Gets or sets the id of the layout.
        /// </summary>
        public string LayoutId { get; set; }

        /// <summary>
        /// Gets or sets the name of the layout.
        /// </summary>
        public string LayoutName { get; set; }

        /// <summary>
        /// Gets or sets the description of the layout.
        /// </summary>
        public string LayoutDescription { get; set; }

        /// <summary>
        /// Gets the amount of triggers that exist on this layout.
        /// </summary>
        public int TriggerCount() => this.GetTriggerProperties().Length;

        /// <summary>
        /// Returns wether there is multiple keys that have the same bindings.
        /// </summary>
        public bool CheckDoubleAssigned()
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Returns all the trigger properties on this class.
        /// </summary>
        protected PropertyInfo[] GetTriggerProperties()
        {
            return this.GetType()
              .GetProperties(BindingFlags.Public | BindingFlags.Instance)
              .Where(HasTriggerAttribute)
              .ToArray();
        }

        private bool HasTriggerAttribute(PropertyInfo info)
        {
            return info.GetCustomAttributes(typeof(TriggerAttribute), true).Length > 0;
        }
    }
}
