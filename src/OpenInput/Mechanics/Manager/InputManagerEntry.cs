namespace OpenInput.Mechanics.Manager
{
    using OpenInput.Mechanics.Combo;

    public abstract class InputManagerEntry
    {
        internal InputManagerEntry(
            InputSystem inputSystem,
            ComboTracker comboTracker)
        {
            this.InputSystem = inputSystem;
            this.ComboTracker = comboTracker;
        }

        public InputSystem InputSystem { get; }
        public ComboTracker ComboTracker { get; }
    }
}
