namespace OpenInput.Test
{
    class MyInputManager : InputManager<MyPlayer>
    {
        public MyInputManager(DeviceSet deviceSet)
            : base (deviceSet)
        {
            this.AllowRegister = true;
            this.MaxControllers = 4;
        }

        protected override void OnJoin(MyPlayer player)
        {

        }
    }
}