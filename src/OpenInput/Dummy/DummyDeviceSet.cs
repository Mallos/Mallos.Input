namespace OpenInput.Dummy
{
    public class DummyDeviceSet
        : DeviceSet<DummyKeyboard, DummyMouse, DummyGamePad>
    {
        public DummyDeviceSet()
            : base("Dummy",
                  new DummyKeyboard(),
                  new DummyMouse(),
                  CreateGamePads())
        {
        }

        private static DummyGamePad[] CreateGamePads()
        {
            var result = new DummyGamePad[4];
            for (int i = 0; i < 4; i++)
            {
                result[i] = new DummyGamePad(i);
            }
            return result;
        }
    }
}
