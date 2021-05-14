namespace Mallos.Input.Trackers
{
    using Mallos.Input.Touch;

    /// <summary>
    /// Basic mouse tracker, takes the old and the new state then compares them.
    /// </summary>
    public class BasicTouchDeviceTracker : BasicDeviceTracker<ITouchTracker, TouchCollection>, ITouchTracker
    {
        /// <summary>
        /// Initialize a new <see cref="BasicTouchDeviceTracker"/> class.
        /// </summary>
        /// <param name="touchDevice"></param>
        public BasicTouchDeviceTracker(ITouchDevice touchDevice)
            : base(touchDevice)
        {
        }

        /// <summary>
        /// Initialize a new <see cref="BasicTouchDeviceTracker"/> class.
        /// </summary>
        /// <param name="touchDevice"></param>
        public BasicTouchDeviceTracker(IDevice<ITouchTracker, TouchCollection> touchDevice)
            : base(touchDevice)
        {
        }

        protected override void Track(TouchCollection newState, TouchCollection oldState)
        {
            // TODO: Watch for changes
        }
    }
}
