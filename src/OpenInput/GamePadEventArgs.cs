namespace OpenInput
{
    using System;

    /// <summary>
    /// GamePad event args.
    /// </summary>
    public class GamePadEventArgs : EventArgs
    {
        /// <summary>
        /// Initializes a new instance of <see cref="GamePadEventArgs"/>.
        /// </summary>
        public GamePadEventArgs(GamePadState state)
        {
            this.State = state;
        }

        /// <summary>
        /// Gets the state as of when the event occurs.
        /// </summary>
        public GamePadState State { get; }
    }
}
