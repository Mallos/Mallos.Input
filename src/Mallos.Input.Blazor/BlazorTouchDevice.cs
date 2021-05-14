namespace Mallos.Input.Blazor
{
    using Mallos.Input.Touch;
    using Mallos.Input.Trackers;
    using System;
    using System.Linq;
    using System.Numerics;
    using System.Threading.Tasks;

    public class BlazorTouchDevice : BlazorDevice, ITouchDevice
    {
        /// <inheritdoc />
        public bool IsGestureAvailable => true;

        /// <inheritdoc />
        public string Name => "Blazor Touch";

        /// <inheritdoc />
        public ITouchTracker CreateTracker()
            => new BasicTouchDeviceTracker(this);

        /// <inheritdoc />
        public TouchCollection GetCurrentState()
            => BlazorTouchDeviceState.GetSnapshot();

        /// <inheritdoc />
        public GestureSample ReadGesture()
        {
            throw new NotImplementedException();
        }
    }

    public static class BlazorTouchDeviceState
    {
        private static TouchLocation[] touchLocations = Array.Empty<TouchLocation>();

        public static TouchCollection GetSnapshot()
        {
            return new TouchCollection(touchLocations);
        }

        public static ValueTask OnTouch(BlazorTouchPoint[] points)
        {
            touchLocations = points.Select(x =>
            {
                var state = TouchLocationState.Moved;
                var position = new Vector2(x.X, x.Y);

                return new TouchLocation((int)x.Identifier, state, position);
            }).ToArray();

            return ValueTask.CompletedTask;
        }
    }
}
