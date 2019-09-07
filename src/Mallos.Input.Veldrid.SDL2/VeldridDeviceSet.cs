namespace Mallos.Input
{
    using Veldrid;

    public class VeldridDeviceSet
        : DeviceSet<VeldridKeyboard, VeldridMouse, VeldridGamePad>
    {
        public VeldridDeviceSet()
            : base("Veldrid",
                  new VeldridKeyboard(),
                  new VeldridMouse(),
                  null)
        {
        }

        public void UpdateSnapshot(InputSnapshot snapshot)
        {
            this.GKeyboard.UpdateSnapshot(snapshot);
            this.GMouse.UpdateSnapshot(snapshot);
        }
    }
}
