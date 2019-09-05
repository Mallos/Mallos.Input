namespace Mallos.Input
{
    using Veldrid;

    public abstract class VeldridDevice
    {
        internal abstract void UpdateSnapshot(InputSnapshot snapshot);
    }
}
