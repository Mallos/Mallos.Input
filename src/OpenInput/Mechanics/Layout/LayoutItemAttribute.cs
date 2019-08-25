namespace OpenInput.Mechanics.Layout
{
    using System;

    public class LayoutItemAttribute : Attribute
    {
        public string Name { get; }

        public string Description { get; }

        public string Group { get; }

        public LayoutItemAttribute(
          string name,
          string description = null,
          string group = null)
        {
            this.Name = name;
            this.Description = description;
            this.Group = group;
        }
    }
}
