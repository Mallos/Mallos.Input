namespace OpenInput.Trackers
{
    using System;

    /// <summary>
    /// Basic gamepad tracker, takes the old and the new state then compares them.
    /// </summary>
    public class BasicGamePadTracker : BasicDeviceTracker<IGamePadTracker, GamePadState>, IGamePadTracker
    {
        /// <inheritdoc />
        public event EventHandler<GamePadEventArgs> ButtonDown;

        /// <inheritdoc />
        public event EventHandler<GamePadEventArgs> ButtonUp;

        /// <summary>
        /// Initialize a new <see cref="BasicGamePadTracker"/> class.
        /// </summary>
        /// <param name="keyboard"></param>
        public BasicGamePadTracker(IGamePad gamePad)
            : base(gamePad)
        {

        }

        /// <summary>
        /// Initialize a new <see cref="BasicGamePadTracker"/> class.
        /// </summary>
        /// <param name="keyboard"></param>
        public BasicGamePadTracker(IDevice<IGamePadTracker, GamePadState> gamePad)
            : base(gamePad)
        {

        }

        protected override void Track(GamePadState newState, GamePadState oldState)
        {
            throw new NotImplementedException();
        }
    }
}
