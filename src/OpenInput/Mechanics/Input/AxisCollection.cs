namespace OpenInput.Mechanics.Input
{
    public class AxisCollection : TriggerCollection<InputAxis, float>
    {
        /// <summary>
        /// Helpers methods for making it easier to add a new action.
        /// </summary>
        public void Add(string name, InputKey key, float value)
        {
            base.Add(new InputAxis(name, key, value));
        }

        protected override float OnTriggerDown(InputAxis trigger, float currentValue)
        {
            return currentValue + trigger.Value;
        }
    }
}
