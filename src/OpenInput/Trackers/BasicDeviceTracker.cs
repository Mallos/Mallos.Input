namespace OpenInput.Trackers
{
    using System;

    /// <summary>
    /// Basic device tracker that takes the old and the new state then compares allows for comparison.
    /// </summary>
    /// <typeparam name="TState"></typeparam>
    public abstract class BasicDeviceTracker<TTracker, TState> : ITracker
        where TTracker : ITracker
        where TState : struct
    {
        /// <summary>
        /// The device we are tracking.
        /// </summary>
        public readonly IDevice<TTracker, TState> Device;

        private TState oldState;

        protected BasicDeviceTracker(IDevice<TTracker, TState> device)
        {
            this.Device = device ?? throw new ArgumentNullException(nameof(device));
        }

        public void Update(float elapsedTime)
        {
            var newState = Device.GetCurrentState();
            Track(newState, oldState);
            oldState = newState;
        }

        protected abstract void Track(TState newState, TState oldState);
    }
}
