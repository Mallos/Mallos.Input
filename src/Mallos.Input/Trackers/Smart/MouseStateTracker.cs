namespace Mallos.Input.Trackers.Smart
{
    using System;
    using System.Numerics;

    public class MouseStateTracker : IMouseTracker
    {
        /// <summary>
        /// Gets the current device state.
        /// </summary>
        public MouseState MouseState { get; private set; }

        /// <inheritdoc />
        public event EventHandler<MouseEventArgs> Move;
        
        /// <inheritdoc />
        public event EventHandler<MouseWheelEventArgs> MouseWheel;

        /// <inheritdoc />
        public event EventHandler<MouseButtonEventArgs> MouseDown;

        /// <inheritdoc />
        public event EventHandler<MouseButtonEventArgs> MouseUp;

        public void Update(float elapsedTime)
        {
        }

        public void OnMove(Vector2 position, Vector2 delta)
        {
            this.MouseState = new MouseState(
                (int)position.X,
                (int)position.Y,
                this.MouseState.ScrollWheelValue,
                this.MouseState.PressedButtons);

            if (delta != Vector2.Zero)
            {
                this.Move?.Invoke(this, new MouseEventArgs(this.MouseState));
            }
        }

        public void OnMouseWheel(int delta)
        {
            this.MouseState = new MouseState(
                this.MouseState.X,
                this.MouseState.Y,
                delta,
                this.MouseState.PressedButtons);

            this.MouseWheel?.Invoke(this, new MouseWheelEventArgs(this.MouseState));
        }

        public void OnButtonDown(MouseButtons button)
        {
            this.MouseState = new MouseState(
                this.MouseState.X,
                this.MouseState.Y,
                this.MouseState.ScrollWheelValue,
                this.MouseState.PressedButtons | button);

            this.MouseDown?.Invoke(this, new MouseButtonEventArgs(this.MouseState, button));
        }

        public void OnButtonUp(MouseButtons button)
        {
            this.MouseState = new MouseState(
                this.MouseState.X,
                this.MouseState.Y,
                this.MouseState.ScrollWheelValue,
                this.MouseState.PressedButtons ^ button);

            this.MouseUp?.Invoke(this, new MouseButtonEventArgs(this.MouseState, button));
        }
    }
}
