namespace Mallos.Input.Trackers
{
    using System;

    /// <summary>
    /// Basic gamepad tracker, takes the old and the new state then compares them.
    /// </summary>
    public class BasicGamePadTracker : BasicDeviceTracker<IGamePadTracker, GamePadState>, IGamePadTracker
    {
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

        /// <inheritdoc />
        public event EventHandler<GamePadEventArgs> ButtonDown;

        /// <inheritdoc />
        public event EventHandler<GamePadEventArgs> ButtonUp;

        protected override void Track(GamePadState newState, GamePadState oldState)
        {
            foreach (Buttons button in Enum.GetValues(typeof(Buttons)))
            {
                bool newButton = newState.Buttons.IsButtonDown(button);
                bool oldButton = oldState.Buttons.IsButtonDown(button);

                if (newButton != oldButton)
                {
                    if (newButton)
                    {
                        this.ButtonDown?.Invoke(this, new GamePadEventArgs(newState));
                    }
                    else
                    {
                        this.ButtonUp?.Invoke(this, new GamePadEventArgs(newState));
                    }
                }
            }
        }
    }
}
