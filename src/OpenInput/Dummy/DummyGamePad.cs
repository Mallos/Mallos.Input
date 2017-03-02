namespace OpenInput.Dummy
{
    using OpenInput.Trackers;

    /// <summary>
    /// Dummy GamePad, does nothing.
    /// </summary>
    public class DummyGamePad : IGamePad
    {
        /// <inheritdoc />
        public int Index { get; private set; }

        /// <inheritdoc />
        public string Name => "Dummy GamePad";

        /// <summary>
        /// Initialize a new <see cref="DummyGamePad"/> class.
        /// </summary>
        /// <param name="index"></param>
        public DummyGamePad(int index)
        {
            this.Index = index;
        }

        /// <inheritdoc />
        public IGamePadTracker CreateTracker()
        {
            return new BasicGamePadTracker(this);
        }

        /// <inheritdoc />
        public GamePadState GetCurrentState()
        {
            return GamePadState.Empty;
        }
    }
}