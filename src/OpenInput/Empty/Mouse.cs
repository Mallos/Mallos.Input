namespace OpenInput.Empty
{
    /// <summary>
    /// Empty Mouse, does nothing.
    /// </summary>
    public sealed class Mouse : IMouse
    {
        /// <inheritdoc />
        public string Name => "Empty Mouse";

        /// <inheritdoc />
        public MouseState GetCurrentState()
        {
            return new MouseState();
        }

        /// <inheritdoc />
        public void SetPosition(int x, int y)
        {

        }
    }
}
