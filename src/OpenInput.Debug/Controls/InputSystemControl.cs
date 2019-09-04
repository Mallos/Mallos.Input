namespace OpenInput.Debug.Controls
{
    using OpenInput.Mechanics;

    /// <summary>
    /// Debug Control for getting a insight into the input system.
    /// </summary>
    public partial class InputSystemControl : Control
    {
        public InputSystemControl(InputSystem inputSystem)
        {
            this.InputSystem = inputSystem;
        }

        public InputSystem InputSystem { get; set; }
    }
}
