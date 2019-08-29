namespace OpenInput.Mechanics.Input
{
    public interface IInputTrigger
    {
        /// <summary>
        /// Gets the name associated with this trigger.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Gets the key that trigger this.
        /// </summary>
        InputKey Key { get; }
    }
}