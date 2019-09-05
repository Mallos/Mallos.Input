namespace Mallos.Input.Dummy
{
    public class DummyDeviceSet
        : DeviceSet<DummyKeyboard, DummyMouse, DummyGamePad>
    {
        public DummyDeviceSet()
            : base("Dummy",
                  new DummyKeyboard(),
                  new DummyMouse(),
                  null)
        {
        }
    }
}
