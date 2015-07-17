namespace OpenInput.Empty
{
    /// <summary>
    /// Empty GamePad, does nothing.
    /// </summary>
    public sealed class GamePad : IGamePad
    {
        /// <inheritdoc />
        public string Name => "Empty GamePad";

        /// <inheritdoc />
        public GamePadState GetCurrentState()
        {
            return GetCurrentState(0);
        }

        /// <inheritdoc />
        public GamePadState GetCurrentState(int index)
        {
            return new GamePadState();
        }

        /// <inheritdoc />
        public int GetDevicesCount()
        {
            return 0;
        }
    }
}
