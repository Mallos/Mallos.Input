namespace Mallos.Input.Mechanics.Input
{
    public class ActionCollection : TriggerCollection<InputAction, bool>
    {
        /// <summary>
        /// Helpers methods for making it easier to add a new action.
        /// </summary>
        public void Add(string name, InputKey key)
        {
            base.Add(new InputAction(name, key));
        }

        protected override bool OnTriggerDown(InputAction trigger, bool currentValue)
        {
            return true;
        }
    }
}
