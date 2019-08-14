namespace OpenInput
{
    using OpenInput.Trackers;

    public class VeldridGamePad : IGamePad
    {
        public VeldridGamePad(int index)
        {
            this.Index = index;
            //this.Name = OpenTK.Input.GamePad.GetName(Index);
        }

        /// <inheritdoc />
        public int Index { get; }

        /// <inheritdoc />
        public string Name { get; private set; }

        /// <inheritdoc />
        public IGamePadTracker CreateTracker()
        {
            throw new System.NotImplementedException();
        }

        /// <inheritdoc />
        public GamePadState GetCurrentState()
        {
            throw new System.NotImplementedException();
        }
    }
}
