namespace Mallos.Input.Blazor
{
    using System;
    using System.Linq;
    using System.Numerics;
    using System.Threading.Tasks;
    using Mallos.Input.Touch;

    internal class BlazorTouchDeviceState
    {
        private TouchLocation[] touchLocations = Array.Empty<TouchLocation>();

        public TouchCollection GetSnapshot()
        {
            return new TouchCollection(touchLocations);
        }

        public ValueTask OnTouch(BlazorTouchPoint[] points)
        {
            touchLocations = points.Select(x =>
            {
                var state = TouchLocationState.Moved;
                var position = new Vector2(x.X, x.Y);

                return new TouchLocation((int) x.Identifier, state, position);
            }).ToArray();

            return ValueTask.CompletedTask;
        }
    }
}
