namespace OpenInput
{
    using System;

    /// <summary>
    /// Device event args.
    /// </summary>
    public class DeviceChangedEventArgs : EventArgs
    {
        /// <summary>
        /// Initializes a new instance of <see cref="DeviceChangedEventArgs"/>.
        /// </summary>
        public DeviceChangedEventArgs(IDevice device)
        {
            this.Device = device;
        }

        /// <summary>
        /// Gets the associated device.
        /// </summary>
        public IDevice Device { get; }
    }
}
