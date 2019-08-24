namespace OpenInput.Mechanics
{
    interface IController
    {
        void AttachInput(InputSystem inputSystem);
        void DeattachInput(InputSystem inputSystem);
    }

    /// <summary>
    /// A complete manager of handling joining and leaving input devices to
    /// controllers. This removes all the need for handling all odd cases
    /// when allowing a new player to leave and exit the game.
    /// </summary>
    class InputManager<TController>
        where TController : IController
    {
        public InputManager(DeviceSet deviceSet)
        {
        }

        public bool AllowRegister { get; set; } = false;

        public int MaxControllers { get; set; } = 4;

        protected virtual bool CanJoin(InputKey key, IDevice device)
        {
            return false;
        }

        protected virtual void OnJoin(TController controller)
        {
        }

        protected virtual void OnLeave(TController controller)
        {
        }
    }
}
