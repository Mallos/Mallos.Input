namespace OpenInput.Mechanics.Layout
{
    public class ActionTriggerAttribute : TriggerAttribute
    {
        public ActionTriggerAttribute(
            string name,
            bool isReadOnly = false)
            : base(name, isReadOnly)
        {
        }
    }
}
