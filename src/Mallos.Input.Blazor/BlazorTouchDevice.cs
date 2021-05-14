namespace Mallos.Input.Blazor
{
    using System;
    using Mallos.Input.Blazor.Components;
    using Mallos.Input.Touch;
    using Mallos.Input.Trackers;

    public class BlazorTouchDevice : BlazorDevice, ITouchDevice
    {
        /// <inheritdoc />
        public bool IsGestureAvailable => true;

        /// <inheritdoc />
        public string Name => "Blazor Touch";

        public BlazorTouchDevice(MInputWrapperComponent component)
            : base(component)
        {
        }

        /// <inheritdoc />
        public ITouchTracker CreateTracker()
            => new BasicTouchDeviceTracker(this);

        /// <inheritdoc />
        public TouchCollection GetCurrentState()
            => this.component.touchDeviceState.GetSnapshot();

        /// <inheritdoc />
        public GestureSample ReadGesture()
        {
            throw new NotImplementedException();
        }
    }
}
