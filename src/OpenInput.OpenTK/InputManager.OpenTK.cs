namespace OpenInput
{
    using OpenTK;

    partial class InputManager
    {
        private GameWindow gameWindow;

        public InputManager(GameWindow gameWindow)
        {
            SetSingleton();

            this.gameWindow = gameWindow;

            PlatformUpdate();
        }
        
        public void PlatformUpdate()
        {

        }
    }
}
