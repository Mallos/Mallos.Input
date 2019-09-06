namespace Mallos.Input.InputSnapshot
{
    using Veldrid;

    public abstract class VeldridDevice
    {
        internal abstract void UpdateSnapshot(InputSnapshot snapshot);
    }
}
