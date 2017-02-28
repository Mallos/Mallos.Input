namespace OpenInput.Dummy
{
    public class DummyDeviceSet : DeviceSet
    {
        public DummyDeviceSet()
            : base("Dummy",
                  new OpenInput.Dummy.DummyKeyboard(),
                  new OpenInput.Dummy.DummyMouse(),
                  (int index) => { return new OpenInput.Dummy.DummyGamePad(index); })
        {

        }
    }
}
