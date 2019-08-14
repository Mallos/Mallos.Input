namespace OpenInput
{
    using OpenInput.Trackers;
    using System.Collections.Generic;
    using System.Linq;
    using Veldrid;

    public class VeldridMouse : VeldridDevice, IMouse
    {
        private readonly HashSet<MouseButton> pressedButtons = new HashSet<MouseButton>();
        private MouseState currentState = new MouseState();

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
            foreach (var key in snapshot.MouseEvents)
            {
                if (key.Down)
                {
                    this.pressedButtons.Add(key.MouseButton);
                }
                else
                {
                    this.pressedButtons.Remove(key.MouseButton);
                }
            }

            this.currentState = new MouseState(
                (int)snapshot.MousePosition.X,
                (int)snapshot.MousePosition.Y,
                (int)snapshot.WheelDelta,
                pressedButtons.Contains(MouseButton.Left),
                pressedButtons.Contains(MouseButton.Middle),
                pressedButtons.Contains(MouseButton.Right),
                pressedButtons.Contains(MouseButton.Button1),
                pressedButtons.Contains(MouseButton.Button2));
        }
    }
}
