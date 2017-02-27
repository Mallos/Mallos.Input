namespace OpenInput.Dummy
{
    using System;
    using OpenInput.Trackers;

    /// <summary>
    /// Dummy Mouse, does nothing.
    /// </summary>
    public sealed class DummyMouse : IMouse
    {
        /// <inheritdoc />
        public string Name => "Dummy Mouse";
        
        /// <inheritdoc />
        public IMouseTracker CreateTracker()
        {
            return new BasicMouseTracker(this);
        }

        /// <inheritdoc />
        public MouseState GetCurrentState()
        {
            return MouseState.Empty;
        }

        /// <inheritdoc />
        public void GetPosition(out int x, out int y)
        {
            y = x = 0;
        }

        /// <inheritdoc />
        public void SetPosition(int x, int y)
        {

        }
    }
}
