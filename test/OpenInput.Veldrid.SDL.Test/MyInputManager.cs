namespace OpenInput.Test
{
    class MyPlayer : IController
    {
        public void AttachInput(InputManagerEntry entry)
        {

        }

        public void DeattachInput(InputManagerEntry entry)
        {

        }

        public void UpdateController(float elapsedTime, InputSystem input)
        {

        }
    }

    class MyInputManager : InputManager<MyPlayer>
    {
        public MyInputManager(DeviceSet deviceSet)
            : base (deviceSet)
        {
            this.AllowRegister = false;
            this.MaxControllers = 4;
        }

        protected override MyPlayer CreateController(InputKey key, IDevice device)
        {
            return new MyPlayer();
        }

        protected override void OnJoin(MyPlayer player)
        {

        }
    }
}