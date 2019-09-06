namespace Mallos.Input.InputSnapshot
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
            this.Keyboard.UpdateSnapshot(snapshot);
            this.Mouse.UpdateSnapshot(snapshot);
        }
    }
}
