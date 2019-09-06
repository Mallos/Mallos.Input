namespace Mallos.Input.InputSnapshot
{
    using Mallos.Input.Trackers;
    using Veldrid;

    public class VeldridMouse : VeldridDevice, IMouse
    {
        private MouseState currentState = new MouseState();
        private MouseButtons buttons = MouseButtons.Empty;

        /// <inheritdoc />
        public string Name => "Veldrid Mouse";

        /// <inheritdoc />
        public IMouseTracker CreateTracker() => new BasicMouseTracker(this);

        /// <inheritdoc />
        public MouseState GetCurrentState() => this.currentState;

        /// <inheritdoc />
        public void GetPosition(out int x, out int y)
        {
            x = this.currentState.X;
            y = this.currentState.Y;
        }

        /// <inheritdoc />
        public void SetPosition(int x, int y)
        {
            throw new System.NotImplementedException();
        }

        internal override void UpdateSnapshot(InputSnapshot snapshot)
        {
            foreach (MouseEvent key in snapshot.MouseEvents)
            {
                if (key.Down)
                {
                    this.buttons |= key.MouseButton.ConvertMouseButtons();
                }
                else
                {
                    this.buttons ^= key.MouseButton.ConvertMouseButtons();
                }
            }

            this.currentState = new MouseState(
                (int) snapshot.MousePosition.X,
                (int) snapshot.MousePosition.Y,
                (int) snapshot.WheelDelta,
                this.buttons);
        }
    }
}
