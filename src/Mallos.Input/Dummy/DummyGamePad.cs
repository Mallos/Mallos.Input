namespace Mallos.Input.Dummy
{
    using Mallos.Input.Trackers;

    /// <summary>
    /// Dummy GamePad, does nothing.
    /// </summary>
    public class DummyGamePad : IGamePad
    {
        /// <summary>
        /// Initialize a new <see cref="DummyGamePad"/> class.
        /// </summary>
        /// <param name="index"></param>
        public DummyGamePad(int index)
        {
            this.Index = index;
        }

        /// <inheritdoc />
        public int Index { get; private set; }

        /// <inheritdoc />
        public string Name => "Dummy GamePad";

        /// <inheritdoc />
        public IGamePadTracker CreateTracker() => new BasicGamePadTracker(this);

        /// <inheritdoc />
        public GamePadState GetCurrentState() => GamePadState.Empty;
    }
}