namespace Mallos.Input.MonoGame
{
    using Mallos.Input.Trackers;

    public class MonoGameGamePad : MonoGameDevice, IGamePad
    {
        public int Index => throw new System.NotImplementedException();

        public string Name => throw new System.NotImplementedException();

        public IGamePadTracker CreateTracker()
        {
            throw new System.NotImplementedException();
        }

        public GamePadState GetCurrentState()
        {
            throw new System.NotImplementedException();
        }
    }
}
