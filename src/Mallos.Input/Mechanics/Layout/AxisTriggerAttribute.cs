namespace Mallos.Input.Mechanics.Layout
{
    public class AxisTriggerAttribute : TriggerAttribute
    {
        public float Value { get; }

        public AxisTriggerAttribute(
            string name,
            float value,
            bool isReadOnly = false)
            : base(name, isReadOnly)
        {
            this.Value = value;
        }
    }
}
