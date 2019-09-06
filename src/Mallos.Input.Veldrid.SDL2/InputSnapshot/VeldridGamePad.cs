namespace Mallos.Input.InputSnapshot
{
    using System;
    using Mallos.Input.Trackers;
    using Veldrid;

    public class VeldridGamePad : VeldridDevice, IGamePad
    {
        private GamePadState currentState = new GamePadState();

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
        public IGamePadTracker CreateTracker() => new BasicGamePadTracker(this);

        /// <inheritdoc />
        public GamePadState GetCurrentState() => this.currentState;

        internal override void UpdateSnapshot(InputSnapshot snapshot)
        {
            throw new NotSupportedException();
        }
    }
}
