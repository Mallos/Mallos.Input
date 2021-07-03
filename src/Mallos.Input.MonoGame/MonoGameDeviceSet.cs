namespace Mallos.Input.MonoGame
{
    public class MonoGameDeviceSet
        : DeviceSet<
            MonoGameKeyboard,
            MonoGameMouse,
            MonoGameGamePad
        >
    {
        public MonoGameDeviceSet()
            : base("MonoGame",
                  new MonoGameKeyboard(),
                  new MonoGameMouse(),
                  null)
        {
        }
    }
}
