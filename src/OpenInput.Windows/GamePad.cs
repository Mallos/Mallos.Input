namespace OpenInput
{
    using System;

    public class GamePad : IGamePad
    {
        public string Name
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public GamePadState GetCurrentState()
        {
            return GetCurrentState(0);
        }

        public GamePadState GetCurrentState(int index)
        {
            throw new NotImplementedException();
        }
    }
}
