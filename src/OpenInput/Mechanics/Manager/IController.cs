namespace OpenInput.Mechanics.Manager
{
    public interface IController
    {
        void AttachInput(InputManagerEntry entry);

        void DeattachInput(InputManagerEntry entry);

        void UpdateController(float elapsedTime, InputSystem input);
    }
}
