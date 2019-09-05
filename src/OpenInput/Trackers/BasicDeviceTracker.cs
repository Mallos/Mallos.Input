namespace OpenInput.Trackers
{
    using System;

    /// <summary>
    /// Basic device tracker that takes the old and the new state then compares allows for comparison.
    /// </summary>
    /// <typeparam name="TTracker"></typeparam>
    /// <typeparam name="TState"></typeparam>
    public abstract class BasicDeviceTracker<TTracker, TState> : ITracker
        where TTracker : ITracker
        where TState : struct
    {
        private TState oldState;

        /// <summary>
        /// Initializes a new instance of <see cref="BasicDeviceTracker"/>.
        /// </summary>
        protected BasicDeviceTracker(IDevice<TTracker, TState> device)
        {
            this.Device = device ?? throw new ArgumentNullException(nameof(device));
        }

        /// <summary>
        /// The device we are tracking.
        /// </summary>
        public IDevice<TTracker, TState> Device { get; }

        public void Update(float elapsedTime)
        {
            TState newState = this.Device.GetCurrentState();
            this.Track(newState, this.oldState);
            this.oldState = newState;
        }

        protected abstract void Track(TState newState, TState oldState);
    }
}
