namespace Mallos.Input.MonoGame
{
    using Mallos.Input.Trackers;

    public class MonoGameKeyboard : MonoGameDevice, IKeyboard
    {
        public TextInput TextInput => throw new System.NotImplementedException();

        public string Name => throw new System.NotImplementedException();

        public IKeyboardTracker CreateTracker()
            => new BasicKeyboardTracker(this);

        public KeyboardState GetCurrentState()
        {
            throw new System.NotImplementedException();
        }
    }
}
