namespace Mallos.Input.Touch
{
    using System;

    /// <summary>
    /// A representation of data from a multitouch gesture over a span of time.
    /// </summary>
    public struct GestureSample
    {
        // TODO: Add Delta/2 & Position/2

        /// <summary>
        /// The type of gesture in a multitouch gesture sample.
        /// </summary>
        public GestureType GestureType { get; internal set; }

        /// <summary>
        /// Holds the starting time for this touch gesture sample.
        /// </summary>
        public TimeSpan Timestamp { get; internal set; }
    }
}
