namespace OpenInput.Mechanics.Layout
{
    using System;

    public abstract class TriggerAttribute : Attribute
    {
        public string Name { get; }

        public TriggerAttribute(string name)
        {
            this.Name = name;
        }
    }

    public class ActionTriggerAttribute : TriggerAttribute
    {
        public ActionTriggerAttribute(string name)
          : base(name)
        {
        }
    }

    public class AxisTriggerAttribute : TriggerAttribute
    {
        public float Value { get; }
        
        public AxisTriggerAttribute(string name, float value)
          : base(name)
        {
            this.Value = value;
        }
    }
}
