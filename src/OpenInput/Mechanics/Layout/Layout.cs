namespace OpenInput.Mechanics.Layout
{
    using System.Linq;
    using System.Reflection;
    using System.Collections.Generic;

    /// <summary>
    /// A class that makes it easier to handle input layouts for your players.
    /// </summary>
    /// <remark>
    /// All settings are required to have a trigger attribute.
    /// </remark>
    public abstract class Layout
    {
        private readonly PropertyInfo[] settingsProperties;

        /// <summary>
        /// Initializes a new instance of <see cref="Layout"/>.
        /// </summary>
        /// <param name="layoutId">The id of the current layout.</param>
        public Layout(string layoutId)
        {
            this.LayoutId = layoutId;
            this.settingsProperties = GetSettingProperties();
        }

        /// <summary>
        /// Gets or sets the id of the layout. Makes it easier when saving the layout.
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
        /// Gets the amount of settings that exist on this layout.
        /// </summary>
        public int SettingsCount => this.settingsProperties.Length;

        /// <summary>
        /// Returns wether or not a key is used in the current layout.
        /// </summary>
        public bool IsKeyUsed(InputKey key, out LayoutSetting[] settings)
        {
            var result = new List<LayoutSetting>();

            foreach (var property in this.settingsProperties)
            {
                var value = (InputKeys)property.GetValue(this);
                if (value.HasKey(key) && CreateLayoutSetting(property, out var setting, out _))
                {
                    result.Add(setting);
                }
            }

            if (result.Count > 0)
            {
                settings = result.ToArray();
                return true;
            }

            settings = null;
            return false;
        }

        /// <summary>
        /// Clear and apply the current layout to the <see cref="InputSystem"/>.
        /// </summary>
        public void Apply(InputSystem inputSystem)
        {
            inputSystem.Clear();

            foreach (var setting in this.settingsProperties)
            {
                var value = (InputKeys)setting.GetValue(this);
                var triggerAttr = setting.GetCustomAttribute<TriggerAttribute>();

                if (triggerAttr is ActionTriggerAttribute actionAttr)
                {
                    foreach (var key in value.Keys)
                    {
                        inputSystem.Actions.Add(new InputAction(actionAttr.Name, key));
                    }
                }

                if (triggerAttr is AxisTriggerAttribute axisAttr)
                {
                    foreach (var key in value.Keys)
                    {
                        inputSystem.Axis.Add(new InputAxis(axisAttr.Name, key, axisAttr.Value));
                    }
                }
            }
        }

        /// <summary>
        /// Returns a grouped dictionary of all the settings.
        /// </summary>
        public IReadOnlyDictionary<string, List<LayoutSetting>> GetSettings()
        {
            var dictionary = new Dictionary<string, List<LayoutSetting>>();

            foreach (var property in this.settingsProperties)
            {
                if (CreateLayoutSetting(property, out var setting, out var group))
                {
                    if (dictionary[group] == null)
                    {
                        dictionary[group] = new List<LayoutSetting>();
                    }

                    dictionary[group].Add(setting);
                }
            }

            return dictionary;
        }

        /// <summary>
        /// Returns all the setting properties on this class.
        /// </summary>
        protected PropertyInfo[] GetSettingProperties()
        {
            return this.GetType()
              .GetProperties(BindingFlags.Public | BindingFlags.Instance)
              .Where(CSharpExtensions.HasCustomAttribute<TriggerAttribute>)
              .ToArray();
        }

        private bool CreateLayoutSetting(
            PropertyInfo propertyInfo,
            out LayoutSetting setting,
            out string group)
        {
            var attr = propertyInfo.GetCustomAttribute<LayoutItemAttribute>();
            if (attr == null)
            {
                setting = default;
                group = null;
                return false;
            }

            var value = (InputKeys)propertyInfo.GetValue(this);
            setting = new LayoutSetting(this, propertyInfo, attr.Name, attr.Description, value);
            group = attr.Group;
            return true;
        }
    }
}
