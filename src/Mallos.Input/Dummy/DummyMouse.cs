namespace Mallos.Input.Dummy
{
    using Mallos.Input.Trackers;

    /// <summary>
    /// Dummy Mouse, does nothing.
    /// </summary>
    public sealed class DummyMouse : IMouse
    {
        /// <inheritdoc />
        public string Name => "Dummy Mouse";

        /// <inheritdoc />
        public IMouseTracker CreateTracker() => new BasicMouseTracker(this);

        /// <inheritdoc />
        public MouseState GetCurrentState() => MouseState.Empty;

        /// <inheritdoc />
        public void GetPosition(out int x, out int y) => y = x = 0;

        /// <inheritdoc />
        public void SetPosition(int x, int y)
        {
        }
    }
}
