namespace OpenInput
{
    using OpenInput.Trackers;
    using Veldrid;

    public class VeldridMouse : VeldridDevice, IMouse
    {
        /// <inheritdoc />
        public string Name => "Veldrid Mouse";

        /// <inheritdoc />
        public IMouseTracker CreateTracker() => new BasicMouseTracker(this);

        /// <inheritdoc />
        public MouseState GetCurrentState()
        {
            // FIXME:
            return new MouseState();
        }

        /// <inheritdoc />
        public void GetPosition(out int x, out int y)
        {
            throw new System.NotImplementedException();
        }

        /// <inheritdoc />
        public void SetPosition(int x, int y)
        {
            throw new System.NotImplementedException();
        }

        internal override void UpdateSnapshot(InputSnapshot snapshot)
        {

        }
    }
}
