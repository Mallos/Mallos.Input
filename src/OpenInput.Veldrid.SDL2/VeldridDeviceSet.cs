namespace OpenInput
{
    using Veldrid;

    public class VeldridDeviceSet : DeviceSet
    {
        public VeldridDeviceSet()
            : base("Veldrid",
                  new VeldridKeyboard(),
                  new VeldridMouse(),
                  index => new VeldridGamePad(index))
        {
        }

        public void UpdateSnapshot(InputSnapshot snapshot)
        {
            // FIXME: We shouldn't cast it
            ((VeldridDevice)this.Keyboard).UpdateSnapshot(snapshot);
            ((VeldridDevice)this.Mouse).UpdateSnapshot(snapshot);
        }
    }
}
