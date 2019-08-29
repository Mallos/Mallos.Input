namespace OpenInput
{
    using Veldrid;

    public class VeldridDeviceSet
        : DeviceSet<VeldridKeyboard, VeldridMouse, VeldridGamePad>
    {
        public VeldridDeviceSet()
            : base("Veldrid",
                  new VeldridKeyboard(),
                  new VeldridMouse(),
                  CreateGamePads())
        {
        }

        public void UpdateSnapshot(InputSnapshot snapshot)
        {
            this.Keyboard.UpdateSnapshot(snapshot);
            this.Mouse.UpdateSnapshot(snapshot);
        }

        private static VeldridGamePad[] CreateGamePads()
        {
            var result = new VeldridGamePad[4];
            for (int i = 0; i < 4; i++)
            {
                result[i] = new VeldridGamePad(i);
            }
            return result;
        }
    }
}
