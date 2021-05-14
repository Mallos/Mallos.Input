namespace Mallos.Input.Blazor
{
    using Mallos.Input.Blazor.Components;
    using Mallos.Input.Trackers;

    public class BlazorMouse : BlazorDevice, IMouse
    {
        /// <inheritdoc />
        public string Name => "Blazor Mouse";

        public BlazorMouse(MInputWrapperComponent component)
            : base(component)
        {
        }

        /// <inheritdoc />
        public IMouseTracker CreateTracker()
            => new BasicMouseTracker(this);

        /// <inheritdoc />
        public MouseState GetCurrentState()
            => this.component.mouseState.GetSnapshot();

        /// <inheritdoc />
        public void GetPosition(out int x, out int y)
        {
            MouseState currentState = this.GetCurrentState();
            x = currentState.X;
            y = currentState.Y;
        }

        /// <inheritdoc />
        public void SetPosition(int x, int y)
            => throw new System.NotSupportedException();
    }
}
